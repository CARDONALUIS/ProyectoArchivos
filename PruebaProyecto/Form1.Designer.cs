﻿namespace PruebaProyecto
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarDiccionarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diccionarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entidadesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ventanaAtributos = new System.Windows.Forms.ToolStripMenuItem();
            this.RegistroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sMBDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.diccionarioToolStripMenuItem,
            this.RegistroToolStripMenuItem,
            this.sMBDToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(807, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.nuevoToolStripMenuItem,
            this.cerrarDiccionarioToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.AbrirToolStripMenuItem_Click);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.NuevoArchivo);
            // 
            // cerrarDiccionarioToolStripMenuItem
            // 
            this.cerrarDiccionarioToolStripMenuItem.Name = "cerrarDiccionarioToolStripMenuItem";
            this.cerrarDiccionarioToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.cerrarDiccionarioToolStripMenuItem.Text = "Cerrar ";
            this.cerrarDiccionarioToolStripMenuItem.Click += new System.EventHandler(this.CerrarDiccionarioToolStripMenuItem_Click);
            // 
            // diccionarioToolStripMenuItem
            // 
            this.diccionarioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.entidadesToolStripMenuItem,
            this.ventanaAtributos});
            this.diccionarioToolStripMenuItem.Name = "diccionarioToolStripMenuItem";
            this.diccionarioToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.diccionarioToolStripMenuItem.Text = "Diccionario";
            this.diccionarioToolStripMenuItem.Click += new System.EventHandler(this.DiccionarioToolStripMenuItem_Click);
            // 
            // entidadesToolStripMenuItem
            // 
            this.entidadesToolStripMenuItem.Name = "entidadesToolStripMenuItem";
            this.entidadesToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.entidadesToolStripMenuItem.Text = "Entidades";
            this.entidadesToolStripMenuItem.Click += new System.EventHandler(this.ventanaEntidades);
            // 
            // ventanaAtributos
            // 
            this.ventanaAtributos.Name = "ventanaAtributos";
            this.ventanaAtributos.Size = new System.Drawing.Size(125, 22);
            this.ventanaAtributos.Text = "Atributos";
            this.ventanaAtributos.Click += new System.EventHandler(this.VentanaAtributos_Click);
            // 
            // RegistroToolStripMenuItem
            // 
            this.RegistroToolStripMenuItem.Name = "RegistroToolStripMenuItem";
            this.RegistroToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.RegistroToolStripMenuItem.Text = "Registro";
            this.RegistroToolStripMenuItem.Click += new System.EventHandler(this.RegistroToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Archivo Diccionario : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 5;
            // 
            // sMBDToolStripMenuItem
            // 
            this.sMBDToolStripMenuItem.Name = "sMBDToolStripMenuItem";
            this.sMBDToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.sMBDToolStripMenuItem.Text = "SMBD";
            this.sMBDToolStripMenuItem.Click += new System.EventHandler(this.botonVentanSMBD);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 355);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diccionarioToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem entidadesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ventanaAtributos;
        private System.Windows.Forms.ToolStripMenuItem RegistroToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem cerrarDiccionarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sMBDToolStripMenuItem;
    }
}

