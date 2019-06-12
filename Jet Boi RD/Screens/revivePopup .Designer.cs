namespace Jet_Boi_RD.Screens
{
    partial class revivePopup
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
            this.playAgainButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.youFlewLabel = new System.Windows.Forms.Label();
            this.shopButton = new System.Windows.Forms.Button();
            this.reviveButton = new System.Windows.Forms.Button();
            this.coindisp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // playAgainButton
            // 
            this.playAgainButton.BackColor = System.Drawing.Color.LightSlateGray;
            this.playAgainButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playAgainButton.Location = new System.Drawing.Point(1002, 461);
            this.playAgainButton.Name = "playAgainButton";
            this.playAgainButton.Size = new System.Drawing.Size(379, 120);
            this.playAgainButton.TabIndex = 3;
            this.playAgainButton.Text = "Play Again";
            this.playAgainButton.UseVisualStyleBackColor = false;
            this.playAgainButton.Click += new System.EventHandler(this.HomeButton_Click);
            // 
            // homeButton
            // 
            this.homeButton.BackColor = System.Drawing.Color.LightSlateGray;
            this.homeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeButton.Location = new System.Drawing.Point(42, 461);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(379, 120);
            this.homeButton.TabIndex = 1;
            this.homeButton.Text = "Quit";
            this.homeButton.UseVisualStyleBackColor = false;
            this.homeButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // youFlewLabel
            // 
            this.youFlewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.youFlewLabel.ForeColor = System.Drawing.Color.White;
            this.youFlewLabel.Location = new System.Drawing.Point(459, 20);
            this.youFlewLabel.Name = "youFlewLabel";
            this.youFlewLabel.Size = new System.Drawing.Size(507, 210);
            this.youFlewLabel.TabIndex = 91;
            this.youFlewLabel.Text = "TEMP TEXT";
            this.youFlewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // shopButton
            // 
            this.shopButton.BackColor = System.Drawing.Color.LightSlateGray;
            this.shopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shopButton.Location = new System.Drawing.Point(527, 461);
            this.shopButton.Name = "shopButton";
            this.shopButton.Size = new System.Drawing.Size(379, 120);
            this.shopButton.TabIndex = 2;
            this.shopButton.Text = "Shop";
            this.shopButton.UseVisualStyleBackColor = false;
            this.shopButton.Click += new System.EventHandler(this.No_Click);
            // 
            // reviveButton
            // 
            this.reviveButton.BackColor = System.Drawing.Color.LightSlateGray;
            this.reviveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 29F);
            this.reviveButton.Location = new System.Drawing.Point(527, 233);
            this.reviveButton.Name = "reviveButton";
            this.reviveButton.Size = new System.Drawing.Size(379, 191);
            this.reviveButton.TabIndex = 0;
            this.reviveButton.Text = "     Buy Revive         250 coins  ";
            this.reviveButton.UseVisualStyleBackColor = false;
            this.reviveButton.Click += new System.EventHandler(this.Yes_Click);
            // 
            // coindisp
            // 
            this.coindisp.AutoSize = true;
            this.coindisp.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.coindisp.ForeColor = System.Drawing.Color.White;
            this.coindisp.Location = new System.Drawing.Point(39, 43);
            this.coindisp.Name = "coindisp";
            this.coindisp.Size = new System.Drawing.Size(108, 37);
            this.coindisp.TabIndex = 92;
            this.coindisp.Text = "Coins:";
            // 
            // revivePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1435, 641);
            this.Controls.Add(this.coindisp);
            this.Controls.Add(this.youFlewLabel);
            this.Controls.Add(this.homeButton);
            this.Controls.Add(this.shopButton);
            this.Controls.Add(this.playAgainButton);
            this.Controls.Add(this.reviveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "revivePopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "revivePopup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button playAgainButton;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.Label youFlewLabel;
        private System.Windows.Forms.Button shopButton;
        private System.Windows.Forms.Button reviveButton;
        private System.Windows.Forms.Label coindisp;
    }
}