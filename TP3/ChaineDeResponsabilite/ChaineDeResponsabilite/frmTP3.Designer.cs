namespace ChaineDeResponsabilite
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbRecettes = new System.Windows.Forms.ListBox();
            this.btnRechercher = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIngredient = new System.Windows.Forms.TextBox();
            this.btnVider = new System.Windows.Forms.Button();
            this.txtCommande = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCommande = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbRecettes
            // 
            this.lbRecettes.FormattingEnabled = true;
            this.lbRecettes.Location = new System.Drawing.Point(12, 12);
            this.lbRecettes.Name = "lbRecettes";
            this.lbRecettes.Size = new System.Drawing.Size(195, 199);
            this.lbRecettes.TabIndex = 999;
            this.lbRecettes.TabStop = false;
            // 
            // btnRechercher
            // 
            this.btnRechercher.Location = new System.Drawing.Point(213, 55);
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.Size = new System.Drawing.Size(75, 23);
            this.btnRechercher.TabIndex = 3;
            this.btnRechercher.Text = "Rechercher";
            this.btnRechercher.UseVisualStyleBackColor = true;
            this.btnRechercher.Click += new System.EventHandler(this.btnRechercher_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ingrédient :";
            // 
            // txtIngredient
            // 
            this.txtIngredient.Location = new System.Drawing.Point(213, 29);
            this.txtIngredient.Name = "txtIngredient";
            this.txtIngredient.Size = new System.Drawing.Size(100, 20);
            this.txtIngredient.TabIndex = 2;
            // 
            // btnVider
            // 
            this.btnVider.Location = new System.Drawing.Point(213, 84);
            this.btnVider.Name = "btnVider";
            this.btnVider.Size = new System.Drawing.Size(75, 23);
            this.btnVider.TabIndex = 4;
            this.btnVider.Text = "Vider";
            this.btnVider.UseVisualStyleBackColor = true;
            this.btnVider.Click += new System.EventHandler(this.btnVider_Click);
            // 
            // txtCommande
            // 
            this.txtCommande.Location = new System.Drawing.Point(12, 236);
            this.txtCommande.Name = "txtCommande";
            this.txtCommande.Size = new System.Drawing.Size(424, 20);
            this.txtCommande.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Interpréteur de commande - ";
            // 
            // btnCommande
            // 
            this.btnCommande.Location = new System.Drawing.Point(361, 207);
            this.btnCommande.Name = "btnCommande";
            this.btnCommande.Size = new System.Drawing.Size(75, 23);
            this.btnCommande.TabIndex = 1;
            this.btnCommande.Text = "Interpreter";
            this.btnCommande.UseVisualStyleBackColor = true;
            this.btnCommande.Click += new System.EventHandler(this.btnCommande_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnCommande;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 268);
            this.Controls.Add(this.btnCommande);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCommande);
            this.Controls.Add(this.btnVider);
            this.Controls.Add(this.txtIngredient);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRechercher);
            this.Controls.Add(this.lbRecettes);
            this.Name = "Form1";
            this.Text = "TP3-Chaines de Responsabilités - Samuel Goulet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbRecettes;
        private System.Windows.Forms.Button btnRechercher;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIngredient;
        private System.Windows.Forms.Button btnVider;
        private System.Windows.Forms.TextBox txtCommande;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCommande;
    }
}

