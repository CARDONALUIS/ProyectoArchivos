﻿namespace PruebaProyecto
{
    partial class Entidades
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxEntidad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.labelCab = new System.Windows.Forms.Label();
            this.GridEntidades = new System.Windows.Forms.DataGridView();
            this.button4 = new System.Windows.Forms.Button();
            this.buttonModEnti = new System.Windows.Forms.Button();
            this.buttonCamEnt = new System.Windows.Forms.Button();
            this.buttonEliEnti = new System.Windows.Forms.Button();
            this.comboBoxModEnt = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.GridEntidades)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entidades";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cabecera";
            // 
            // textBoxEntidad
            // 
            this.textBoxEntidad.Location = new System.Drawing.Point(97, 55);
            this.textBoxEntidad.Name = "textBoxEntidad";
            this.textBoxEntidad.Size = new System.Drawing.Size(100, 20);
            this.textBoxEntidad.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Entidad";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(232, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Agregar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.botonActualizar);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(254, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 5;
            // 
            // labelCab
            // 
            this.labelCab.AutoSize = true;
            this.labelCab.Location = new System.Drawing.Point(241, 9);
            this.labelCab.Name = "labelCab";
            this.labelCab.Size = new System.Drawing.Size(19, 13);
            this.labelCab.TabIndex = 6;
            this.labelCab.Text = ": 0";
            // 
            // GridEntidades
            // 
            this.GridEntidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridEntidades.Location = new System.Drawing.Point(12, 100);
            this.GridEntidades.Name = "GridEntidades";
            this.GridEntidades.Size = new System.Drawing.Size(776, 288);
            this.GridEntidades.TabIndex = 8;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(328, 51);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "VerActuales";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.VerActualesEnti);
            // 
            // buttonModEnti
            // 
            this.buttonModEnti.Location = new System.Drawing.Point(439, 51);
            this.buttonModEnti.Name = "buttonModEnti";
            this.buttonModEnti.Size = new System.Drawing.Size(75, 23);
            this.buttonModEnti.TabIndex = 12;
            this.buttonModEnti.Text = "Modificar";
            this.buttonModEnti.UseVisualStyleBackColor = true;
            this.buttonModEnti.Click += new System.EventHandler(this.ButtonModEnti_Click);
            // 
            // buttonCamEnt
            // 
            this.buttonCamEnt.Location = new System.Drawing.Point(539, 25);
            this.buttonCamEnt.Name = "buttonCamEnt";
            this.buttonCamEnt.Size = new System.Drawing.Size(75, 23);
            this.buttonCamEnt.TabIndex = 13;
            this.buttonCamEnt.Text = "Cambiar";
            this.buttonCamEnt.UseVisualStyleBackColor = true;
            this.buttonCamEnt.Click += new System.EventHandler(this.ButtonCamEnt_Click);
            // 
            // buttonEliEnti
            // 
            this.buttonEliEnti.Location = new System.Drawing.Point(539, 71);
            this.buttonEliEnti.Name = "buttonEliEnti";
            this.buttonEliEnti.Size = new System.Drawing.Size(75, 23);
            this.buttonEliEnti.TabIndex = 14;
            this.buttonEliEnti.Text = "Elimina";
            this.buttonEliEnti.UseVisualStyleBackColor = true;
            this.buttonEliEnti.Click += new System.EventHandler(this.ButtonEliEnti_Click);
            // 
            // comboBoxModEnt
            // 
            this.comboBoxModEnt.FormattingEnabled = true;
            this.comboBoxModEnt.Location = new System.Drawing.Point(642, 36);
            this.comboBoxModEnt.Name = "comboBoxModEnt";
            this.comboBoxModEnt.Size = new System.Drawing.Size(121, 21);
            this.comboBoxModEnt.TabIndex = 15;
            // 
            // Entidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 399);
            this.Controls.Add(this.comboBoxModEnt);
            this.Controls.Add(this.buttonEliEnti);
            this.Controls.Add(this.buttonCamEnt);
            this.Controls.Add(this.buttonModEnti);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.GridEntidades);
            this.Controls.Add(this.labelCab);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxEntidad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Entidades";
            this.Text = "Entidades";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Entidades_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.GridEntidades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxEntidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelCab;
        private System.Windows.Forms.DataGridView GridEntidades;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button buttonModEnti;
        private System.Windows.Forms.Button buttonCamEnt;
        private System.Windows.Forms.Button buttonEliEnti;
        private System.Windows.Forms.ComboBox comboBoxModEnt;
    }
}