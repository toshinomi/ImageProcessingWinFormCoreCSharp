namespace ImageProcessingWinFormCoreCSharp
{
    partial class FormSettingImageProcessing
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettingImageProcessing));
            this.labelImageProcessingType = new System.Windows.Forms.Label();
            this.cmbBoxImageProcessingType = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelImageProcessingType
            // 
            this.labelImageProcessingType.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelImageProcessingType.Location = new System.Drawing.Point(11, 9);
            this.labelImageProcessingType.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelImageProcessingType.Name = "labelImageProcessingType";
            this.labelImageProcessingType.Size = new System.Drawing.Size(209, 36);
            this.labelImageProcessingType.TabIndex = 0;
            this.labelImageProcessingType.Text = "Image Processing Type";
            this.labelImageProcessingType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbBoxImageProcessingType
            // 
            this.cmbBoxImageProcessingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxImageProcessingType.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbBoxImageProcessingType.FormattingEnabled = true;
            this.cmbBoxImageProcessingType.Location = new System.Drawing.Point(228, 14);
            this.cmbBoxImageProcessingType.Name = "cmbBoxImageProcessingType";
            this.cmbBoxImageProcessingType.Size = new System.Drawing.Size(244, 28);
            this.cmbBoxImageProcessingType.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Window;
            this.btnCancel.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCancel.Location = new System.Drawing.Point(346, 212);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(126, 37);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.OnClickCancel);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.Window;
            this.btnOk.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnOk.Location = new System.Drawing.Point(214, 212);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(126, 37);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.OnClickOk);
            // 
            // FormSettingImageProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbBoxImageProcessingType);
            this.Controls.Add(this.labelImageProcessingType);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "FormSettingImageProcessing";
            this.Text = "Setting Image Processing";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelImageProcessingType;
        private System.Windows.Forms.ComboBox cmbBoxImageProcessingType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}