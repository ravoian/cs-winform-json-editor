using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;


namespace JSONEditor
{
    public partial class Form : System.Windows.Forms.Form
    {
        DataTable dt = new DataTable();
        private JToken _jsonObject;

        public class Branch
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public Merge[] Merges { get; set; }
        }

        public class Merge
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public string Mapping { get; set; }
            public bool Reverse { get; set; }
        }


        public Form()
        {
            InitializeComponent();
            btnGenerateSample.Click += btnGenerateSample_Click;
            btnSaveJSON.Click += btnSaveJSON_Click;
            btnLoadJSON.Click += btnLoadJSON_Click;
            treeViewJson.BeforeLabelEdit += BeforeLabelEdit;
            treeViewJson.AfterLabelEdit += AfterLabelEdit;

            // Allow node labels to be edited
            treeViewJson.LabelEdit = true;
        }

        private void btnGenerateSample_Click(object sender, EventArgs e)
        {
            // Create an example configuration
            var MergeInst = new Merge
            {
                Name = "Target",
                Path = "//Depot/Target",
                Mapping = "Source-to-Target",
                Reverse = false
            };

            Merge[] SampleTargets = { MergeInst, MergeInst };

            var BranchInst = new Branch
            {
                Name = "Source",
                Path = "//Depot/Source",
                Merges = SampleTargets
            };

            // Serialize the object to a JSON string
            string jsonString = JsonSerializer.Serialize(BranchInst);

            InitializeTreeView(jsonString);
        }

        private void btnLoadJSON_Click(object sender, EventArgs e)
        {
            // Open a OpenFileDialog to specify the path and name of the JSON file
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.DefaultExt = "json";
                openFileDialog.AddExtension = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Load the JSON string from the specified file
                        var readJson = File.ReadAllText(openFileDialog.FileName);
                        Branch pp = JsonSerializer.Deserialize<Branch>(readJson);

                        InitializeTreeView(readJson);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to load JSON file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSaveJSON_Click(object sender, EventArgs e)
        {
            // Open a SaveFileDialog to specify the path and name of the JSON file
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "json";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Serialize the JSON object to a string
                        var jsonString = _jsonObject.ToString();

                        // Save the JSON string to the specified file
                        File.WriteAllText(saveFileDialog.FileName, jsonString);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to save JSON file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void InitializeTreeView(String jsonString)
        {
            try
            {
                // Clear previous tree view items
                treeViewJson.Nodes.Clear();

                // Parse JSON
                var json = jsonString;
                _jsonObject = JToken.Parse(json);

                // Load JSON into TreeView & expand nodes
                LoadJsonIntoTreeView(_jsonObject, treeViewJson.Nodes);
                treeViewJson.ExpandAll(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid JSON: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadJsonIntoTreeView(JToken jsonToken, TreeNodeCollection nodes)
        {
            if (jsonToken is JValue value)
            {
                // Handle JSON value
                var node = new TreeNode(value.ToString());
                node.Tag = value;
                nodes.Add(node);
            }
            else if (jsonToken is JObject obj)
            {
                // Handle JSON object
                foreach (var property in obj.Properties())
                {
                    var childNode = new TreeNode(property.Name);
                    childNode.Tag = property;
                    nodes.Add(childNode);
                    LoadJsonIntoTreeView(property.Value, childNode.Nodes);
                }
            }
            else if (jsonToken is JArray array)
            {
                // Handle JSON array
                for (int i = 0; i < array.Count; i++)
                {
                    var childNode = new TreeNode($"[{i}]");
                    childNode.Tag = array[i];
                    nodes.Add(childNode);
                    LoadJsonIntoTreeView(array[i], childNode.Nodes);
                }
            }
        }

        private void BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // Edit only JSON values and not keys
            if (!(e.Node.Tag is JValue))
            {
                e.CancelEdit = true;
            }
        }

        private void AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                // Do nothing when user cancels edit
                return;
            }

            // Get the corresponding JSON token
            var nodeTag = e.Node.Tag;

            if (nodeTag is JValue value)
            {
                // Update the value in the JSON object
                value.Replace(new JValue(e.Label));
            }
            else if (nodeTag is JProperty property)
            {
                // Update the property value
                property.Value = new JValue(e.Label);
            }
        }
    }
}
