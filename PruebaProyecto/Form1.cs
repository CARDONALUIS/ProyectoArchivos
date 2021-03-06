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

    /*
     * Clase que se encarga de mostrar la pantalla principal con sus respectivos controles
    * HOLA PROYECTO ESTOY DENUEVO AQUI 
     * */

    public partial class Form1 : Form
    {
        
        Entidades vEnti;
        Atributos vAtri;
        Datos vDatos;
        FormSMBD vSMBD;
        Diccionario dic;
        int r = 0;
        string nomArchivo;


        //Contructor de la clase instancia el objeto de la clase diccionario
        public Form1()
        {
            InitializeComponent();  
            dic = new Diccionario();


        }
        

        
        //Metodo que se encarga de abrir el archivo del diccionario que se desea usar
        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nomArchivo;
            
            OpenFileDialog abrir = new OpenFileDialog()
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = "BD Files (*.dd)|*.dd;"
            };

            if(abrir.ShowDialog() ==  DialogResult.OK)
            {
                //Guarda el nombre del archivo
                nomArchivo = abrir.FileName;

                label1.Text = "";

                label1.Text = "Archivo Diccionario:" + nomArchivo;
                
                dic.archivo = File.Open(nomArchivo, FileMode.Open, FileAccess.Read);
                dic.nomArchivo = nomArchivo;

                actualizaDiccionario(dic.archivo);

                dic.archivo.Close();
            }
        }
        


        //Metodo que se encarga de guardar en variables del diccionario y guardarlos en estructuras necesarias para su uso en el programa
        public void actualizaDiccionario(FileStream archivo)
        {
            r = 0;
            dic.borraDic();
            //Variables de entidad
            Byte[] idEnti;
            char[] nombreEnti;
            int dirEnt;
            int dirAtri;
            int dirDat;
            int dirSigEnt;
            string noEn = "";
            //Variables de atributos
            Byte[] idAtriL;
            char[] nombreAtriL;
            char[] tipoDL;
            int longiL;
            int dirAtrL;
            int tipoIndL;
            int dirIndL;
            int dirSigAtL;
            string noAt = "";
            bool bandSigEnt = true;
            bool bandSigAtri = true;

            BinaryReader br = new BinaryReader(archivo);


            archivo.Seek(0, SeekOrigin.Begin);
            int dire = br.ReadInt32();
            dic.cab = dire;
            if (dic.cab != -1)
                while (bandSigEnt)
                {
                    archivo.Seek(dire, SeekOrigin.Begin);
                    idEnti = br.ReadBytes(5);
                    r = 0;
                    dire += 5;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    nombreEnti = br.ReadChars(30);
                    r = 0;
                    dire += 30;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    dirEnt = br.ReadInt32();
                    r = 0;
                    dire += 8;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    dirAtri = br.ReadInt32();
                    r = 0;
                    dire += 8;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    dirDat = br.ReadInt32();
                    r = 0;
                    dire += 8;
                    archivo.Seek(dire, SeekOrigin.Begin);
                    dirSigEnt = br.ReadInt32();
                    r = 0;
                    dire = dirAtri;
                    for (int i = 0; i < nombreEnti.Length; i++)
                    {
                        if (char.IsLetter(nombreEnti.ElementAt(i)))
                            noEn += nombreEnti.ElementAt(i);
                    }

                    Entidad ent = new Entidad(idEnti, noEn, dirEnt, dirAtri, dirDat, dirSigEnt);
                    noEn = "";
                    dic.listEntidad.Add(ent);
                    r = 0;
                    if (dirAtri != -1)
                        while (bandSigAtri)
                        {
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            idAtriL = br.ReadBytes(5);
                            dire += 5;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            nombreAtriL = br.ReadChars(30);
                            dire += 30;
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            tipoDL = br.ReadChars(1);
                            dire += 1;
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            longiL = br.ReadInt32();
                            dire += 4;
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            dirAtrL = br.ReadInt32();
                            dire += 8;
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            tipoIndL = Int32.Parse(br.ReadInt32().ToString().Substring(0,1));
                            dire += 4;
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            dirIndL = br.ReadInt32();
                            dire += 8;
                            r = 0;
                            archivo.Seek(dire, SeekOrigin.Begin);
                            dirSigAtL = br.ReadInt32();
                            dire = dirSigAtL;
                            r = 0;
                            for (int i = 0; i < nombreAtriL.Length; i++)
                            {
                                if (char.IsLetter(nombreAtriL.ElementAt(i)) || nombreAtriL.ElementAt(i) == '_')
                                    noAt += nombreAtriL.ElementAt(i);
                            }
                            Atributo atr = new Atributo(idAtriL, noAt, tipoDL[0], longiL, dirAtrL, tipoIndL, dirIndL, dirSigAtL);
                            noAt = "";
                            ent.listAtrib.Add(atr);
                            r = 0;


                            if (dirSigEnt == -1 && dirSigAtL == -1)
                            {
                                bandSigEnt = false;
                                r = 0;
                            }

                            if (dirSigAtL == -1)
                            {
                                bandSigAtri = false;
                                r = 0;
                                dire = dirSigEnt;
                            }
                        }
                    else
                    {

                        dire = dirSigEnt;
                        if (dire == -1)
                            break;
                    }

                    bandSigAtri = true;

                }
            else
                MessageBox.Show("Tu archivo se encuentra vacio!!");

            bandSigEnt = true;
            r = 0;
        }



        //Metodo que se usaba con fines de prueba
        private void DiccionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //vDic.ShowDialog();
        }


        //Metodo para inicialisar las ventanas de entidades atributos y datos
        private void Form1_Load(object sender, EventArgs e)
        {
            //vDic = new VenEntidades();
            vEnti = new Entidades(dic);
            vAtri = new Atributos(dic);
            vDatos = new Datos();
            vSMBD = new FormSMBD();
        }


        //Evento con fines de prueba
        private void Button1_Click(object sender, EventArgs e)
        {

        }

        //Evento con fines de prueba
        private void VerDiccionario(object sender, EventArgs e)
        {

        }

        //Evento para mostrar la ventana de entidades
        private void ventanaEntidades(object sender, EventArgs e)
        { 
            vEnti.actualizaDicc(dic);
            vEnti.ShowDialog();
        }


        //Evento para mostrar la ventana de atributos
        private void VentanaAtributos_Click(object sender, EventArgs e)
        {
            vAtri.actualizaDicc(dic);
            vAtri.ShowDialog();   
        }

        

        //Metodo para crear un nuevo archivo
        private void NuevoArchivo(object sender, EventArgs e)
        {
            MessageBox.Show("No olvides agregar extencion .dd a tu diccionario");

            SaveFileDialog saveDialog = new SaveFileDialog();
            if(saveDialog.ShowDialog() == DialogResult.OK)
            {
                dic.nomArchivo = saveDialog.FileName;                
                dic.archivo = new FileStream(dic.nomArchivo, FileMode.Create);
                dic.archivo.Close();
                label1.Text = "";
                label1.Text = "Archivo Diccionario:" + dic.nomArchivo;
                using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                {
                    bw.Write(dic.cab);
                }
            }
        }


        //Evento con fines de prueba
        private void Guardar_Archivo(object sender, EventArgs e)
        {

        }

        //Evento con fines de prueba
        private void InsertarRegistrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = 0;           
        }


        /*Metodos del archvio de datos, este era con fines de prueba*/
        private void AbrirDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nomArchivoDatos;

            OpenFileDialog abrir = new OpenFileDialog();

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                //Guarda el nombre del archivo
                nomArchivoDatos = abrir.FileName;
                label2.Text = "";
                vDatos.archivoDatos = File.Open(nomArchivoDatos, FileMode.Open, FileAccess.Read);
                vDatos.nomArchivoDatos = nomArchivoDatos;
                vDatos.archivoDatos.Close();
            }
        }


        //Evento para mostrar la ventana de los registros
        private void RegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vDatos.actualizaDicc(dic);
            vDatos.ShowDialog();
        }

        //Evento para cerrar el archivo actual
        private void CerrarDiccionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dic.cab = -1;
            dic.archivo.Close();
            dic.nomArchivo = "";
            label1.Text = "Archivo Diccionario:";

            foreach(Entidad a in dic.listEntidad)
            {
                a.listAtrib.Clear();
            }
            dic.listEntidad.Clear();
            
        }

        private void botonVentanSMBD(object sender, EventArgs e)
        {
            vSMBD.BD = dic;
            r = 0;
            vSMBD.ShowDialog();
        }
    }
}
