
namespace JSONEditor
{
    partial class Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeViewJson = new System.Windows.Forms.TreeView();
            this.btnGenerateSample = new System.Windows.Forms.Button();
            this.btnLoadJSON = new System.Windows.Forms.Button();
            this.btnSaveJSON = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeViewJson
            // 
            this.treeViewJson.LabelEdit = true;
            this.treeViewJson.Location = new System.Drawing.Point(22, 29);
            this.treeViewJson.Name = "treeViewJson";
            this.treeViewJson.Size = new System.Drawing.Size(605, 389);
            this.treeViewJson.TabIndex = 0;
            // 
            // btnGenerateSample
            // 
            this.btnGenerateSample.Location = new System.Drawing.Point(647, 29);
            this.btnGenerateSample.Name = "btnGenerateSample";
            this.btnGenerateSample.Size = new System.Drawing.Size(141, 23);
            this.btnGenerateSample.TabIndex = 1;
            this.btnGenerateSample.Text = "Generate sample data";
            this.btnGenerateSample.UseVisualStyleBackColor = true;
            // 
            // btnLoadJSON
            // 
            this.btnLoadJSON.Location = new System.Drawing.Point(647, 58);
            this.btnLoadJSON.Name = "btnLoadJSON";
            this.btnLoadJSON.Size = new System.Drawing.Size(141, 23);
            this.btnLoadJSON.TabIndex = 2;
            this.btnLoadJSON.Text = "Load JSON File";
            this.btnLoadJSON.UseVisualStyleBackColor = true;
            // 
            // btnSaveJSON
            // 
            this.btnSaveJSON.Location = new System.Drawing.Point(647, 87);
            this.btnSaveJSON.Name = "btnSaveJSON";
            this.btnSaveJSON.Size = new System.Drawing.Size(141, 23);
            this.btnSaveJSON.TabIndex = 3;
            this.btnSaveJSON.Text = "Save JSON File";
            this.btnSaveJSON.UseVisualStyleBackColor = true;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSaveJSON);
            this.Controls.Add(this.btnLoadJSON);
            this.Controls.Add(this.btnGenerateSample);
            this.Controls.Add(this.treeViewJson);
            this.Name = "Form";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewJson;
        private System.Windows.Forms.Button btnGenerateSample;
        private System.Windows.Forms.Button btnLoadJSON;
        private System.Windows.Forms.Button btnSaveJSON;
    }
}

