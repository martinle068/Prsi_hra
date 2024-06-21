namespace Prsi_hra
{
    partial class Form1
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
            pocet_karet_pocitace = new Label();
            tahat_karty = new Button();
            horni_karta = new Button();
            zmenena_barva = new Label();
            b6 = new Button();
            b5 = new Button();
            b4 = new Button();
            pocatecni_pocet_karet = new Label();
            prsi = new Label();
            zprava = new Label();
            kule_tlacitko = new Button();
            zaludy_tlacitko = new Button();
            listy_tlacitko = new Button();
            srdce_tlacitko = new Button();
            predchozi_karta = new Label();
            konec_zprava = new Label();
            hrat_znova = new Button();
            popisek_karta_na_vrchu = new Label();
            vase_karty = new Label();
            SuspendLayout();
            // 
            // pocet_karet_pocitace
            // 
            pocet_karet_pocitace.AutoSize = true;
            pocet_karet_pocitace.Location = new Point(60, 76);
            pocet_karet_pocitace.Name = "pocet_karet_pocitace";
            pocet_karet_pocitace.Size = new Size(124, 15);
            pocet_karet_pocitace.TabIndex = 0;
            pocet_karet_pocitace.Text = "zbyva mi nekolik karet";
            // 
            // tahat_karty
            // 
            tahat_karty.Location = new Point(514, 102);
            tahat_karty.Margin = new Padding(3, 2, 3, 2);
            tahat_karty.Name = "tahat_karty";
            tahat_karty.Size = new Size(82, 22);
            tahat_karty.TabIndex = 1;
            tahat_karty.Text = "tahat karty";
            tahat_karty.UseVisualStyleBackColor = true;
            tahat_karty.Click += tahat_karty_Click;
            // 
            // horni_karta
            // 
            horni_karta.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            horni_karta.Location = new Point(294, 76);
            horni_karta.Margin = new Padding(3, 2, 3, 2);
            horni_karta.Name = "horni_karta";
            horni_karta.Size = new Size(89, 74);
            horni_karta.TabIndex = 2;
            horni_karta.Text = "button1";
            horni_karta.UseVisualStyleBackColor = true;
            // 
            // zmenena_barva
            // 
            zmenena_barva.AutoSize = true;
            zmenena_barva.Location = new Point(60, 102);
            zmenena_barva.Name = "zmenena_barva";
            zmenena_barva.Size = new Size(128, 15);
            zmenena_barva.TabIndex = 3;
            zmenena_barva.Text = "barva nebyla změněna!";
            // 
            // b6
            // 
            b6.Location = new Point(381, 188);
            b6.Margin = new Padding(3, 2, 3, 2);
            b6.Name = "b6";
            b6.Size = new Size(52, 45);
            b6.TabIndex = 4;
            b6.Text = "6";
            b6.UseVisualStyleBackColor = true;
            b6.Click += b6_Click;
            // 
            // b5
            // 
            b5.Location = new Point(323, 188);
            b5.Margin = new Padding(3, 2, 3, 2);
            b5.Name = "b5";
            b5.Size = new Size(52, 45);
            b5.TabIndex = 5;
            b5.Text = "5";
            b5.UseVisualStyleBackColor = true;
            b5.Click += b5_Click;
            // 
            // b4
            // 
            b4.Location = new Point(265, 188);
            b4.Margin = new Padding(3, 2, 3, 2);
            b4.Name = "b4";
            b4.Size = new Size(52, 45);
            b4.TabIndex = 6;
            b4.Text = "4";
            b4.UseVisualStyleBackColor = true;
            b4.Click += b4_Click;
            // 
            // pocatecni_pocet_karet
            // 
            pocatecni_pocet_karet.AutoSize = true;
            pocatecni_pocet_karet.Location = new Point(263, 170);
            pocatecni_pocet_karet.Name = "pocatecni_pocet_karet";
            pocatecni_pocet_karet.Size = new Size(157, 15);
            pocatecni_pocet_karet.TabIndex = 7;
            pocatecni_pocet_karet.Text = "S kolika kartami chcete hrát?";
            // 
            // prsi
            // 
            prsi.AutoSize = true;
            prsi.Font = new Font("Segoe UI", 72F, FontStyle.Regular, GraphicsUnit.Point);
            prsi.Location = new Point(239, 42);
            prsi.Name = "prsi";
            prsi.Size = new Size(205, 128);
            prsi.TabIndex = 8;
            prsi.Text = "Prší";
            // 
            // zprava
            // 
            zprava.AutoSize = true;
            zprava.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            zprava.Location = new Point(60, 32);
            zprava.Name = "zprava";
            zprava.Size = new Size(63, 21);
            zprava.TabIndex = 9;
            zprava.Text = "Zprava";
            // 
            // kule_tlacitko
            // 
            kule_tlacitko.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            kule_tlacitko.Location = new Point(60, 124);
            kule_tlacitko.Margin = new Padding(3, 2, 3, 2);
            kule_tlacitko.Name = "kule_tlacitko";
            kule_tlacitko.Size = new Size(37, 25);
            kule_tlacitko.TabIndex = 10;
            kule_tlacitko.Text = "k";
            kule_tlacitko.UseVisualStyleBackColor = true;
            kule_tlacitko.Click += kule_Click;
            // 
            // zaludy_tlacitko
            // 
            zaludy_tlacitko.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            zaludy_tlacitko.Location = new Point(186, 124);
            zaludy_tlacitko.Margin = new Padding(3, 2, 3, 2);
            zaludy_tlacitko.Name = "zaludy_tlacitko";
            zaludy_tlacitko.Size = new Size(37, 25);
            zaludy_tlacitko.TabIndex = 11;
            zaludy_tlacitko.Text = "z";
            zaludy_tlacitko.UseVisualStyleBackColor = true;
            zaludy_tlacitko.Click += zaludy_Click;
            // 
            // listy_tlacitko
            // 
            listy_tlacitko.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            listy_tlacitko.Location = new Point(144, 124);
            listy_tlacitko.Margin = new Padding(3, 2, 3, 2);
            listy_tlacitko.Name = "listy_tlacitko";
            listy_tlacitko.Size = new Size(37, 25);
            listy_tlacitko.TabIndex = 12;
            listy_tlacitko.Text = "l";
            listy_tlacitko.UseVisualStyleBackColor = true;
            listy_tlacitko.Click += listy_Click;
            // 
            // srdce_tlacitko
            // 
            srdce_tlacitko.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            srdce_tlacitko.Location = new Point(102, 124);
            srdce_tlacitko.Margin = new Padding(3, 2, 3, 2);
            srdce_tlacitko.Name = "srdce_tlacitko";
            srdce_tlacitko.Size = new Size(37, 25);
            srdce_tlacitko.TabIndex = 13;
            srdce_tlacitko.Text = "s";
            srdce_tlacitko.UseVisualStyleBackColor = true;
            srdce_tlacitko.Click += srdce_Click;
            // 
            // predchozi_karta
            // 
            predchozi_karta.AutoSize = true;
            predchozi_karta.Location = new Point(294, 151);
            predchozi_karta.Name = "predchozi_karta";
            predchozi_karta.Size = new Size(91, 15);
            predchozi_karta.TabIndex = 14;
            predchozi_karta.Text = "předchozí karta:";
            // 
            // konec_zprava
            // 
            konec_zprava.AutoSize = true;
            konec_zprava.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            konec_zprava.Location = new Point(200, 124);
            konec_zprava.Name = "konec_zprava";
            konec_zprava.Size = new Size(93, 37);
            konec_zprava.TabIndex = 15;
            konec_zprava.Text = "konec";
            konec_zprava.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // hrat_znova
            // 
            hrat_znova.Location = new Point(309, 285);
            hrat_znova.Margin = new Padding(3, 2, 3, 2);
            hrat_znova.Name = "hrat_znova";
            hrat_znova.Size = new Size(86, 22);
            hrat_znova.TabIndex = 16;
            hrat_znova.Text = "hrát znova";
            hrat_znova.UseVisualStyleBackColor = true;
            hrat_znova.Click += hrat_znova_Click;
            // 
            // popisek_karta_na_vrchu
            // 
            popisek_karta_na_vrchu.AutoSize = true;
            popisek_karta_na_vrchu.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            popisek_karta_na_vrchu.Location = new Point(271, 55);
            popisek_karta_na_vrchu.Name = "popisek_karta_na_vrchu";
            popisek_karta_na_vrchu.Size = new Size(113, 20);
            popisek_karta_na_vrchu.TabIndex = 17;
            popisek_karta_na_vrchu.Text = "karta na vrchu:";
            // 
            // vase_karty
            // 
            vase_karty.AutoSize = true;
            vase_karty.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            vase_karty.Location = new Point(10, 149);
            vase_karty.Name = "vase_karty";
            vase_karty.Size = new Size(67, 15);
            vase_karty.TabIndex = 18;
            vase_karty.Text = "vaše karty:";
            vase_karty.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(vase_karty);
            Controls.Add(popisek_karta_na_vrchu);
            Controls.Add(hrat_znova);
            Controls.Add(konec_zprava);
            Controls.Add(predchozi_karta);
            Controls.Add(srdce_tlacitko);
            Controls.Add(listy_tlacitko);
            Controls.Add(zaludy_tlacitko);
            Controls.Add(kule_tlacitko);
            Controls.Add(zprava);
            Controls.Add(prsi);
            Controls.Add(pocatecni_pocet_karet);
            Controls.Add(b4);
            Controls.Add(b5);
            Controls.Add(b6);
            Controls.Add(zmenena_barva);
            Controls.Add(horni_karta);
            Controls.Add(tahat_karty);
            Controls.Add(pocet_karet_pocitace);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label pocet_karet_pocitace;
        private Button tahat_karty;
        private Button horni_karta;
        private Label zmenena_barva;
        private Button b6;
        private Button b5;
        private Button b4;
        private Label pocatecni_pocet_karet;
        private Label prsi;
        private Label zprava;
        private Button kule_tlacitko;
        private Button zaludy_tlacitko;
        private Button listy_tlacitko;
        private Button srdce_tlacitko;
        private Label predchozi_karta;
        private Label konec_zprava;
        private Button hrat_znova;
        private Label popisek_karta_na_vrchu;
        private Label vase_karty;
    }
}