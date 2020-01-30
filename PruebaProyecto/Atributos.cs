﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaProyecto
{
    public partial class Atributos : Form
    {
        public Diccionario dic;
        int r = 0;
        public int contAtributo = 0;
        Entidad ent;
        public Atributos(Diccionario dic)
        {
            InitializeComponent();
     

            inisializaColumnas();
            comboBoxModAtri.Visible = false;
            cambiaAtribu.Visible = false;
            buttonEliAtri.Visible = false;
        }

        public void actualizaDicc(Diccionario d)
        {
            dic = d;
            r = 0;

            comboBoxEntidades.Items.Clear();
            for (int i = 0; i < dic.listEntidad.Count; i++)
            {
                comboBoxEntidades.Items.Add(dic.listEntidad.ElementAt(i).nombre);
            }
        }

        private void ActualizarAtributo(object sender, EventArgs e)
        {
            //Obtenemos la entidad actual
            int entSel = comboBoxEntidades.SelectedIndex;

            r = 0;
            ent = dic.listEntidad.ElementAt(entSel);
            Atributo atriActual;

            String nomAtri = "";

            nomAtri = textBoxNomAtri.Text;

           
/*
           if (ent.listAtrib.Count == 0)
            {
                ent.varSigAtri = (int)ent.dirAtri+ dic.tamAtrib;
                //ent.dirAtri = ent.dirEnti + dic.tamEntidad;
                int dirAtri;


                    dirAtri = (int)dic.listEntidad.ElementAt(dic.listEntidad.Count - 1).dirEnti + dic.tamEntidad;
                //(int)ent.dirEnti+dic.tamEntidad
                Atributo atributo = new Atributo(85, nomAtri, comboBoxAtrTipo.SelectedItem.ToString().ElementAt(0), Int32.Parse(textBoxAtriLong.Text), dirAtri, Int32.Parse(comboBoxAtrTip_Ind.SelectedIndex.ToString()), -1, ent.varSigAtri);
                ent.listAtrib.Add(atributo);
                atriActual = atributo;
            }
            else
            {*/
                r = 0;
            //ent.dirAtri = ent.varSigAtri;

            dic.archivo = File.Open(dic.nomArchivo, FileMode.Open, FileAccess.Read);
            long dirAtri = dic.archivo.Length;

            dic.archivo.Close(); 

               r = 0;
            ent.varSigAtri = ent.varSigAtri + dic.tamAtrib;

            ent.dirAtri = dirAtri;
                Atributo atributo = new Atributo(85, nomAtri, comboBoxAtrTipo.SelectedItem.ToString().ElementAt(0), Int32.Parse(textBoxAtriLong.Text), /*(int)ent.dirAtri*/(int)dirAtri, Int32.Parse(comboBoxAtrTip_Ind.SelectedIndex.ToString()), -1,(int) dirAtri+dic.tamAtrib);
                ent.listAtrib.Add(atributo);
                atriActual = atributo;
            //}
            actualizaUltima(ent);
            actualizaGridAtri(ent);

            //atributo = ent.listAtrib.Find(x => x.nombre == nomAtri);

            escribeAtrib(atributo, ent);

            r = 0;
        }

        public void escribeAtrib(Atributo atr, Entidad ent)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
            {
                if (ent.listAtrib.Count == 1)
                {
                    bw.Seek((int)ent.dirEnti + 48, SeekOrigin.Begin);
                    bw.Write(ent.dirAtri);
                    r = 0;
                }





                int contBytChar = 0;

                bw.Seek((int)atr.dirAtri, SeekOrigin.Begin);

                bw.Write(atr.id_atri);
                bw.Write("");

                bw.Write(atr.nombre);
                if ((atr.nombre.Length) % 2 == 0)
                    while (contBytChar < (35 - atr.nombre.Length) / 2)
                    {
                        bw.Write("-");
                        contBytChar++;
                    }
                else
                {
                    while (contBytChar < (34 - atr.nombre.Length) / 2)
                    {
                        bw.Write("-");
                        contBytChar++;
                    }
                    bw.Write("");
                }

                contBytChar = 0;
                r = 0;



                bw.Write(atr.tipo);
                bw.Write(atr.longitud);
                r = 0;
                bw.Write(atr.dirAtri);
                bw.Write(Int32.Parse(atr.tipoIndi.ToString().ElementAt(0).ToString()));
                bw.Write(atr.dirIndi);
                bw.Write(atr.dirSigAtri);

                /*for (int i = 0; i < ent.listAtrib.Count(); i++)
                {

                }*/
                if (ent.listAtrib.Count != 1)
                {
                    int index = ent.listAtrib.FindIndex(x => x.nombre == atr.nombre);
                    bw.Seek((int)ent.listAtrib.ElementAt(index - 1).dirAtri + 65, SeekOrigin.Begin);
                    bw.Write(atr.dirAtri);
                }

            } 
        }

        public void actualizaGridAtri(Entidad ent)
        {
            GridAtributos.Columns.Clear();
            GridAtributos.Rows.Clear();
            inisializaColumnas();

            for (int i = 0; i < ent.listAtrib.Count; i++)
            {
                GridAtributos.Rows.Add();
                r = 0;
                GridAtributos.Rows[i].Cells[0].Value = ent.listAtrib.ElementAt(i).id_atri;
                r = 0;
                GridAtributos.Rows[i].Cells[1].Value = ent.listAtrib.ElementAt(i).nombre;
                GridAtributos.Rows[i].Cells[2].Value = ent.listAtrib.ElementAt(i).tipo;
                GridAtributos.Rows[i].Cells[3].Value = ent.listAtrib.ElementAt(i).longitud;
                GridAtributos.Rows[i].Cells[4].Value = ent.listAtrib.ElementAt(i).dirAtri;
                GridAtributos.Rows[i].Cells[5].Value = ent.listAtrib.ElementAt(i).tipoIndi;
                GridAtributos.Rows[i].Cells[6].Value = ent.listAtrib.ElementAt(i).dirIndi;
                GridAtributos.Rows[i].Cells[7].Value = ent.listAtrib.ElementAt(i).dirSigAtri;
            }
            r = 0;
        }

        public void actualizaUltima(Entidad ent)
        {
            for (int i = 0; i < ent.listAtrib.Count; i++)
            {
                if (i == ent.listAtrib.Count - 1)
                {
                    ent.listAtrib.ElementAt(i).dirSigAtri = -1;
                }
                else
                {    
                    ent.listAtrib.ElementAt(i).dirSigAtri = (int)ent.listAtrib.ElementAt(i+1).dirAtri;
                }
            }
        }

        private void Atributos_FormClosing(object sender, FormClosingEventArgs e)
        {
            GridAtributos.Columns.Clear();

            GridAtributos.Rows.Clear();
            inisializaColumnas();

            /*for (int i = ent.listAtrib.Count; i > 0; i--)
            {
                GridAtributos.Rows[i].clear
            }*/
            comboBoxModAtri.Visible = false;
        }

        public void inisializaColumnas()
        {
            dic = dic;

            DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
            Columna1.HeaderText = "ID";
            GridAtributos.Columns.Add(Columna1);

            DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
            Columna2.HeaderText = "Atributo";
            GridAtributos.Columns.Add(Columna2);

            DataGridViewTextBoxColumn Columna3 = new DataGridViewTextBoxColumn();
            Columna3.HeaderText = "Tipo";
            GridAtributos.Columns.Add(Columna3);

            DataGridViewTextBoxColumn Columna4 = new DataGridViewTextBoxColumn();
            Columna4.HeaderText = "Longitud";
            GridAtributos.Columns.Add(Columna4);

            DataGridViewTextBoxColumn Columna5 = new DataGridViewTextBoxColumn();
            Columna5.HeaderText = "Dir_Atri";
            GridAtributos.Columns.Add(Columna5);

            DataGridViewTextBoxColumn Columna6 = new DataGridViewTextBoxColumn();
            Columna6.HeaderText = "Tipo_Ind";
            GridAtributos.Columns.Add(Columna6);

            DataGridViewTextBoxColumn Columna7 = new DataGridViewTextBoxColumn();
            Columna7.HeaderText = "Dir_Ind";
            GridAtributos.Columns.Add(Columna7);

            DataGridViewTextBoxColumn Columna8 = new DataGridViewTextBoxColumn();
            Columna8.HeaderText = "Dir_Sig_Atri";
            GridAtributos.Columns.Add(Columna8);
        }

        private void VerActualesAtri(object sender, EventArgs e)
        {
            GridAtributos.Columns.Clear();
            GridAtributos.Rows.Clear();
            inisializaColumnas();

            int entSel = comboBoxEntidades.SelectedIndex;

            r = 0;
            ent = dic.listEntidad.ElementAt(entSel);

            for (int i = 0; i < ent.listAtrib.Count; i++)
            {
                GridAtributos.Rows.Add();
                r = 0;
                GridAtributos.Rows[i].Cells[0].Value = ent.listAtrib.ElementAt(i).id_atri;
                r = 0;
                GridAtributos.Rows[i].Cells[1].Value = ent.listAtrib.ElementAt(i).nombre;
                GridAtributos.Rows[i].Cells[2].Value = ent.listAtrib.ElementAt(i).tipo;
                GridAtributos.Rows[i].Cells[3].Value = ent.listAtrib.ElementAt(i).longitud;
                GridAtributos.Rows[i].Cells[4].Value = ent.listAtrib.ElementAt(i).dirAtri;
                GridAtributos.Rows[i].Cells[5].Value = ent.listAtrib.ElementAt(i).tipoIndi;
                GridAtributos.Rows[i].Cells[6].Value = ent.listAtrib.ElementAt(i).dirIndi;
                GridAtributos.Rows[i].Cells[7].Value = ent.listAtrib.ElementAt(i).dirSigAtri;
            }
        }

        private void ModificarEnti(object sender, EventArgs e)
        {
            //.Visible = false;
            //comboBoxEntidades.Visible = false;
            
            comboBoxModAtri.Visible = true;
            cambiaAtribu.Visible = true;
            buttonEliAtri.Visible = true;

            comboBoxModAtri.Items.Clear();
            
            for (int i = 0; i < ent.listAtrib.Count; i++)
            {
                comboBoxModAtri.Items.Add(ent.listAtrib.ElementAt(i).nombre);
            }

            textBoxNomAtri.Text = "";
            comboBoxAtrTipo.Text = "";
            textBoxAtriLong.Text = "";
            comboBoxAtrTip_Ind.Text = "";


            r = 0;

        }

        private void cambiaAtributo(object sender, EventArgs e)
        {
            int inAM = comboBoxModAtri.SelectedIndex;

            Atributo atM = ent.listAtrib.ElementAt(inAM);

            atM.nombre = textBoxNomAtri.Text  ;
            atM.tipo = comboBoxAtrTipo.Text.ElementAt(0);
            atM.longitud = Int32.Parse(textBoxAtriLong.Text);
            atM.tipoIndi = Int32.Parse(comboBoxAtrTip_Ind.SelectedIndex.ToString());


            r = 0;
                //atM.nombre = textBoxNomAtri.Text;
            r = 0;
        }

        private void eliminaAtri(object sender, EventArgs e)
        {
            int inAM = comboBoxModAtri.SelectedIndex;

            //Atributo atM = ent.listAtrib.ElementAt(inAM);

            /*for(int i = 0; i < ent.listAtrib.Count;i++)
            {

            }*/
            using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
            {
                if (inAM - 1 != -1 && inAM + 1 < ent.listAtrib.Count())
                {
                    bw.Seek((int)ent.listAtrib.ElementAt(inAM - 1).dirAtri+65, SeekOrigin.Begin);
                    bw.Write(ent.listAtrib.ElementAt(inAM + 1).dirAtri);
                    r = 0;
                }
                else
                {
                    if (inAM + 1 == ent.listAtrib.Count())
                    {
                        //int indEnt = dic.listEntidad.FindIndex(x => x.nombre == ent.nombre);
                        //if(dic.ele)
                        bw.Seek((int)ent.listAtrib.ElementAt(inAM - 1).dirAtri + 65, SeekOrigin.Begin);
                        bw.Write(-1);
                        r = 0;
                        //bw.Seek((int)ent.listAtrib.ElementAt(inAM - 1).dirAtri, SeekOrigin.Begin);
                        //bw.Write(ent.listAtrib.ElementAt(inAM + 1).dirAtri);
                    }
                    else
                    {
                        bw.Seek((int)ent.dirEnti+ 48, SeekOrigin.Begin);
                        bw.Write(ent.listAtrib.ElementAt(1).dirAtri);
                        r = 0;
                    }
                }
            }
            //dic.actualizaDiccionario(dic.archivo);
            dic.actualizaDiccionario(dic.archivo);
            buttonEliAtri.Visible = false;
            cambiaAtribu.Visible = false;
            comboBoxModAtri.Visible = false;
            r = 0;
        }
    }
}