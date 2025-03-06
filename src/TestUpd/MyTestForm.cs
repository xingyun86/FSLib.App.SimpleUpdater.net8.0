
namespace TestUpd
{
    public class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
            buttonUpgrade.Click += (_s, _e) => 
            {
                FSLib.App.SimpleUpdater.Updater.CheckUpdateSimple("http://localhost:8080/{0}", "update_c.xml");
            };
        }
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
            this.SuspendLayout();
            this.components = new System.ComponentModel.Container();
            buttonUpgrade.Text = "Éý¼¶";
            buttonUpgrade.Location = new Point(10,10);
            buttonUpgrade.AutoSize = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "TestForm";
            this.Controls.Add(buttonUpgrade);
            this.ResumeLayout();
        }
        private Button buttonUpgrade = new Button();
        #endregion
    }
}
