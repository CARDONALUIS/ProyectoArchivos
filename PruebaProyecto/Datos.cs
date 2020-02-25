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
    public partial class Datos : Form
    {
        Diccionario dic;
        public FileStream archivoDatos;
        public String nomArchivoDatos;
        public int tamYFormDat;
        int r;
        public List<FileStream> listEntDat ;
        Entidad entAct;
        int lonRegisAct = 16;
        bool bandElim = false;
        bool bandModi = false;
        int posAtrBus = 0;
        bool bandAtrBus = false;
        

        public Datos()
        {
            InitializeComponent();
            listEntDat = new List<FileStream>();
            AplicaCambio.Visible = false;
        }
        
        public void actualizaDicc(Diccionario d)
        {
            int r = 0;
            dic = d;

            foreach(Entidad a in dic.listEntidad)
            {
                comboBoxEntiDatos.Items.Add(a.nombre);
            }
        }

        public void creaArchivosDat(Entidad ent)
        {
            ent.archivoDat = (new FileStream(ent.nombre+".dat", FileMode.Create));
            entAct.archivoDat.Close();

            /*int contAtrLon = 0;

            foreach (Atributo a in entAct.listAtrib)
            {
                contAtrLon += a.longitud;

            }


            entAct.longAtributos = contAtrLon+16;*/
        }


        public void GeneraArchivos()
        {

        }
        
        private void GuardaRegistros_Click(object sender, EventArgs e)
        {
            string valor = "";

            if (entAct != null)
            {
                entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
                long lonArDat = entAct.archivoDat.Length;
                //Registro reg = new Registro((int)lonArDat, -1);
                //entAct.listReg.Add(reg);                     
                entAct.archivoDat.Close();

                RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count-1].Cells[0].Value = (int)lonArDat; 
                r = 0;


                using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.nombre + ".dat", FileMode.Open)))
                {
                    bw.Seek((int)lonArDat, SeekOrigin.Begin);
                    bw.Write(lonArDat);
                    r = 0;
                    for (int col = 0; col < RegistroRellDataGrid.Rows[0].Cells.Count; col++)
                    {
                        r = 0;
                        Atributo act = entAct.listAtrib.ElementAt(col);
                        r = 0;
                        RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[col + 1].Value = RegistroRellDataGrid.Rows[0].Cells[col].Value.ToString();
                        valor = RegistroRellDataGrid.Rows[0].Cells[col].Value.ToString();

                        if (act.tipo == 'C')
                        {
                            bw.Write(valor.ToCharArray());
                            int contChar = 0;
                            r = 0;

                            while (contChar < act.longitud - valor.Length)
                            {
                                bw.Write('-');
                                contChar++;
                                r = 0;
                            }
                            r = 0;
                            contChar = 0;
                        }
                        else
                        {
                            bw.Write(Int32.Parse(valor));
                            r = 0;
                        }

                    }
                    //bw.Write((long)-1);
                    //RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[entAct.listAtrib.Count + 1].Value = -1;

                    r = 0;
                    bw.Write((long)-1);
                    RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[entAct.listAtrib.Count + 1].Value = -1;

                    if (!bandAtrBus)
                        compruebaFinal(bw);
                    //RegisInserdataGridView.Sort(RegisInserdataGridView.Columns[posAtrBus], ListSortDirection.Ascending);


                    
                    
                    r = 0;
                }

                //pasaDatosAGrid();

                actualizaPrimerRegEnt(entAct);

                if (bandAtrBus)
                {
                    ordenaPorClv();
                    //ordenaPorClv((int)lonArDat, RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[posAtrBus].Value.ToString(), entAct.archivoDat);
                }


                MessageBox.Show("Tu registros fueron guardados satisfactoriamente");
            limpiaGridRellReg();
            }
            else
                MessageBox.Show("Elige una entidad");

        }

        public void ordenaPorClv()
        {
            int pru = 0, pru2 = 0;
            //if(char.IsDigit())
            //if(int.TryParse())
            /*if(entAct.listAtrib.ElementAt(posAtrBus-1).tipo == 'C')
             {

             }*/

            //while()

            //for(int i = 0; i < RegisInserdataGridView.Rows.Count; i++)
            //{
            string algo2 = "";

            //string valor = RegisInserdataGridView.Rows[2].Cells[posAtrBus].Value.ToString();
            r = 0;


            string cadCveAct;

            // "<" La cadena 1 va antes de la cadena 2
            entAct.archivoDat = File.Open(entAct.nombre + ".dat", FileMode.Open);
            BinaryReader br = new BinaryReader(entAct.archivoDat);



            //entAct.archivoDat.Seek(entAct.dirDat+entAct.posCveBus, SeekOrigin.Begin);

            //int posActReg = (int)entAct.dirDat + entAct.posCveBus;

            int posUltReg = (int)entAct.archivoDat.Length - entAct.longAtributos;


            r = 0;


            entAct.archivoDat.Seek(posUltReg + entAct.posCveBus, SeekOrigin.Begin);
            if(entAct.tipoCveBus =='C')
            {
                cadCveAct = new string(br.ReadChars(10));
            }
            else
            {
                cadCveAct = br.ReadInt32().ToString();
                
            }




            int posRegCom = (int)entAct.dirDat;
            
            //while()

            r = 0;

            entAct.archivoDat.Seek(posRegCom+entAct.posCveBus, SeekOrigin.Begin);
            string cadComp = "";
            int dataGridRen = 0;
            bool bandUlt = false;

            if(entAct.tipoCveBus == 'C')
            {
                cadComp = new string(br.ReadChars(10));
                r = 0;
               
                while (string.CompareOrdinal(cadComp, cadCveAct) < 0)
                {
                    r = 0;

                    int posRegAct = posRegCom;
                    
                    entAct.archivoDat.Seek(posRegCom+entAct.longAtributos-8, SeekOrigin.Begin);
                    posRegCom = posRegCom + entAct.longAtributos - 8;

                    //int valor = br.ReadInt32();
                    int direSigReg = br.ReadInt32();

                    r = 0;

                    if(direSigReg == -1)//llego al registro final
                    {
                        r = 0;
                        entAct.archivoDat.Close();
                        using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoDat.Name, FileMode.Open)))
                        {
                            RegisInserdataGridView.Rows[dataGridRen].Cells[entAct.listAtrib.Count + 1].Value = posUltReg;
                            RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[entAct.listAtrib.Count + 1].Value = direSigReg;
                           
                            r = 0;
                            int p = posRegAct + entAct.longAtributos - 8;
                            r = 0;

                            bw.Seek(posRegAct + entAct.longAtributos-8, SeekOrigin.Begin);
                            bw.Write(posUltReg);
                            bandUlt = true;
                            
                        }
                        entAct.archivoDat = File.Open(entAct.nombre + ".dat", FileMode.Open);
                        break;
                        
                    }
                    else
                    {
                        r = 0;
                        entAct.archivoDat.Seek(direSigReg + entAct.posCveBus, SeekOrigin.Begin);
                        cadComp = new string(br.ReadChars(10));
                        posRegCom = direSigReg;
                        r = 0;
                    }

                    dataGridRen++;

                    r = 0;
                }
                
                r = 0;
                if(string.CompareOrdinal(cadComp, cadCveAct)  == 0)
                {
                    //Son iguales
                    r = 0;
                }
                else
                if(!bandUlt)
                {
                    //Va despues de la actual
                    r = 0;
                    entAct.archivoDat.Close();
                    using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoDat.Name, FileMode.Open)))
                    {
                        

                        r = 0;
                        //int p = posRegAct + entAct.longAtributos - 8;
                        r = 0;
                        int a = posRegCom - 8;

                        r = 0;
                        bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[dataGridRen - 1].Cells[0].Value.ToString())+entAct.longAtributos-8, SeekOrigin.Begin);

                        //int nuevaDir = br.ReadInt32();
                        //bw.Seek(posRegCom - 8, SeekOrigin.Begin);
                        bw.Write(posUltReg);
                        


                        a = posUltReg  - 8;
                        r = 0;
                        bw.Seek(posUltReg + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write(posRegCom);

                        RegisInserdataGridView.Rows[dataGridRen - 1].Cells[entAct.listAtrib.Count + 1].Value = posUltReg;
                        RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count - 1].Cells[entAct.listAtrib.Count + 1].Value = posRegCom;

                    }
                    entAct.archivoDat = File.Open(entAct.nombre + ".dat", FileMode.Open);


                }


                dataGridRen = 0;
            }
            else
            {

            }


            entAct.archivoDat.Close();



            /*using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.archivoDat.Name, FileMode.Open)))
            {
                



            }*/






            r = 0;

            
            /*
            for(int i = 0; i < RegisInserdataGridView.Rows.Count; i++)
            {
                r = 0;
                
                if(i!=0)
                if (Int32.Parse(RegisInserdataGridView.Rows[i].Cells[0].Value.ToString()) == dirAtr)
                {
                        RegisInserdataGridView.Rows[i].Cells[entAct.listAtrib.Count + 1].Value = RegisInserdataGridView.Rows[i - 1].Cells[entAct.listAtrib.Count + 1].Value;
                        RegisInserdataGridView.Rows[i - 1].Cells[entAct.listAtrib.Count + 1].Value = RegisInserdataGridView.Rows[i].Cells[0].Value;
                        
                       
                    using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.nombre + ".dat", FileMode.Open)))
                    {
                        pru = (int)RegisInserdataGridView.Rows[i - 1].Cells[0].Value + entAct.longAtributos - 8;
                        pru2 = Int32.Parse(RegisInserdataGridView.Rows[i - 1].Cells[entAct.listAtrib.Count + 1 ].Value.ToString());
                        r = 0;

                        bw.Seek((int)RegisInserdataGridView.Rows[i - 1].Cells[0].Value + entAct.longAtributos - 8, SeekOrigin.Begin); 
                        bw.Write(Int32.Parse(RegisInserdataGridView.Rows[i - 1].Cells[entAct.listAtrib.Count + 1].Value.ToString()));

                        pru = (int)RegisInserdataGridView.Rows[i].Cells[0].Value + entAct.longAtributos - 8;
                        pru2 = Int32.Parse(RegisInserdataGridView.Rows[i].Cells[entAct.listAtrib.Count + 1].Value.ToString());
                        r = 0;

                        bw.Seek((int)RegisInserdataGridView.Rows[i].Cells[0].Value + entAct.longAtributos - 8, SeekOrigin.Begin);
                        bw.Write(Int32.Parse(RegisInserdataGridView.Rows[i].Cells[entAct.listAtrib.Count + 1].Value.ToString()));                       
                    }
                    break;
                }
            }

            */

            //RegisInserdataGridView.Sort(RegisInserdataGridView.Columns[posAtrBus],ListSortDirection.Ascending);



            /*
            int posRenAnt = 0;

            
            while (string.CompareOrdinal(RegisInserdataGridView.Rows[posRenAnt].Cells[posAtrBus].Value.ToString(), cadAct) < 0)
            {                
                algo2 = RegisInserdataGridView.Rows[posRenAnt].Cells[posAtrBus].Value.ToString();

                r = 0;
                posRenAnt++;

            }
            string algo = RegisInserdataGridView.Rows[posRenAnt].Cells[posAtrBus].Value.ToString();

            r = 0;


            if (string.CompareOrdinal(RegisInserdataGridView.Rows[posRenAnt].Cells[posAtrBus].Value.ToString(), cadAct) == 0 && posRenAnt != 0)
            {
                //MessageBox.Show("Soy igual");

            }
            else//Si va despues
            {
                //RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count].Cells[entAct.listAtrib.Count - 1].Value = RegisInserdataGridView.Rows[posRenAnt].Cells[entAct.listAtrib.Count - 1].Value;
                //RegisInserdataGridView.Rows[posRenAnt].Cells[entAct.listAtrib.Count - 1].Value = RegisInserdataGridView.Rows[RegisInserdataGridView.Rows.Count].Cells[0].Value;


                //MessageBox.Show("" + posRenAnt);
            }
            */
    
            //posRenAnt = 0;
            //}
        }

        public void actualizaPrimerRegEnt(Entidad entAct)
        {
            entAct.archivoDat = File.Open(entAct.nombre + ".dat", FileMode.Open);
            BinaryReader br = new BinaryReader(entAct.archivoDat);
            long valorPri = br.ReadInt32();

            r = 0;
            if (valorPri >= 0)
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                {
                    r = 0;
                    bw.Seek((int)entAct.dirEnti + 51, SeekOrigin.Begin);
                    bw.Write(valorPri);
                    entAct.dirDat = valorPri;
                    r = 0;
                }
            }
            entAct.archivoDat.Close();
        }



        public void compruebaFinal(BinaryWriter bw)
        {
            for (int i = 1; i < RegisInserdataGridView.Rows.Count; i++)
            {
                r = 0;
                if (i != RegisInserdataGridView.Rows.Count)
                {
                    RegisInserdataGridView.Rows[i-1].Cells[entAct.listAtrib.Count+1].Value = RegisInserdataGridView.Rows[i].Cells[0].Value;
                    bw.Seek(Int32.Parse(RegisInserdataGridView.Rows[i - 1].Cells[0].Value.ToString()) + entAct.longAtributos - 8, SeekOrigin.Begin);
                    bw.Write(Int32.Parse(RegisInserdataGridView.Rows[i].Cells[0].Value.ToString()));
                }
            }

            r = 0;
        }

        public void limpiaGridRellReg()
        {
            RegistroRellDataGrid.Rows.Clear();
            RegistroRellDataGrid.Columns.Clear();
        }

        public void limpiaGridInsertadosReg()
        {
            RegisInserdataGridView.Rows.Clear();
            RegisInserdataGridView.Columns.Clear();
        }

        private void CambiaEntiReg(object sender, EventArgs e)
        {

            limpiaGridRellReg();
            limpiaGridInsertadosReg();
            Object inEn = comboBoxEntiDatos.SelectedItem;
            r = 0;

            if (inEn != null)
            {
                //Columna 1 del grid de registrosRellenados
                DataGridViewTextBoxColumn Columna1 = new DataGridViewTextBoxColumn();
                Columna1.HeaderText ="Dir_Registro";
                RegisInserdataGridView.Columns.Add(Columna1);


                Entidad entReg = dic.listEntidad.Find(x => x.nombre == inEn.ToString());
                entAct = entReg;

                posAtrBus = 0;
                foreach (Atributo a in entAct.listAtrib)
                {
                    if (a.tipoIndi == 1)
                    {
                        MessageBox.Show("El atributo de busqueda es " + a.nombre);
                        bandAtrBus = true;
                        entAct.tipoCveBus = a.tipo;
                        break;
                    }
                    else
                        posAtrBus+= a.longitud;
                }
                entAct.posCveBus = posAtrBus+8;

                r = 0;
                foreach (Atributo a in entReg.listAtrib)
                {
                    lonRegisAct += a.longitud;
                }
                entAct.longAtributos = lonRegisAct;
                r = 0;

                foreach (Atributo a in entReg.listAtrib)
                {
                    //Columna 1 del grid de registros por rellenar
                    DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
                    Columna2.HeaderText = a.nombre;
                    RegistroRellDataGrid.Columns.Add(Columna2);

                    //Columnas del grid de registrosRellenados
                    DataGridViewTextBoxColumn Columna3 = new DataGridViewTextBoxColumn();
                    Columna3.HeaderText = a.nombre;
                    RegisInserdataGridView.Columns.Add(Columna3);
                }
                RegistroRellDataGrid.Rows.Add();

                //Columna ultima del grid de registrosRellenados
                DataGridViewTextBoxColumn Columna4 = new DataGridViewTextBoxColumn();
                Columna4.HeaderText = "Dir_Sig_Registro";
                RegisInserdataGridView.Columns.Add(Columna4);

                

                if(entReg.dirDat == -1)
                {
                    r = 0;
                    creaArchivosDat(entReg);
                }
                else
                {
                    r = 0;
                    MessageBox.Show("Ya tiene datos");
                    agregaRegisExistentes(entReg);
                    //RegisInserdataGridView.Sort(RegisInserdataGridView.Columns[posAtrBus], ListSortDirection.Ascending);

                }
            }
            else
                MessageBox.Show("Escoge una entidad");

            lonRegisAct = 0;
        }


        public void agregaRegisExistentes(Entidad ent)
        {
            r = 0;
            int com = 0;
            bool bandSigReg = true;
            int conRen = 0;
            int dirReg = (int)ent.dirDat, dirRegAct, dirSigReg;
            //if()
            
            ent.archivoDat = File.Open(ent.nombre+".dat", FileMode.Open);
            BinaryReader br = new BinaryReader(ent.archivoDat);

            int contAtrLon = 0;

            foreach (Atributo a in entAct.listAtrib)
            {
                contAtrLon += a.longitud;
            }
            entAct.longAtributos = contAtrLon + 16;

            r = 0;
            while (bandSigReg)
            {
                r = 0;
                //ent.archivoDat.Seek(dirReg, SeekOrigin.Begin);
                RegisInserdataGridView.Rows.Add();
                r = 0;
                
                if (conRen == 0)
                    dirReg = (int)entAct.dirDat;
                else
                dirReg = br.ReadInt32();


                

                dirRegAct = dirReg;
                r = 0;
                RegisInserdataGridView.Rows[conRen].Cells[0].Value = dirReg;

                dirReg = dirReg + 8;

                for (int i = 0; i < ent.listAtrib.Count; i++)
                {
                    

                    ent.archivoDat.Seek(dirReg, SeekOrigin.Begin);
                    //com = br.ReadInt32();
                    r = 0;
                    Atributo atr = ent.listAtrib.ElementAt(i);
                    r = 0;
                    switch (atr.tipo)
                    {
                        case 'C':
                            foreach(char a in br.ReadChars(atr.longitud))
                            {
                                if (char.IsDigit(a) || char.IsLetter(a))
                                {
                                    RegisInserdataGridView.Rows[conRen].Cells[i+1].Value += a.ToString();
                                }
                            }
                            String prueba = RegisInserdataGridView.Rows[conRen].Cells[i + 1].Value.ToString();
                            r = 0;
                            break;
                        case 'E':

                            int valor = br.ReadInt32();
                            RegisInserdataGridView.Rows[conRen].Cells[i+1].Value = valor;
                            r = 0;
                            break;
                            
                    }

                    dirReg += atr.longitud;
                    r = 0;
                    ent.archivoDat.Seek(dirReg, SeekOrigin.Begin);

                    if (br.ReadInt32() == -1)
                    {
                        r = 0;
                        //compruebaFinal(bw);
                        bandSigReg = false; 
                    }
                    
                }
                //dirReg = dirReg + 8;
                ent.archivoDat.Seek(dirReg, SeekOrigin.Begin);

                dirSigReg = br.ReadInt32();
                RegisInserdataGridView.Rows[conRen].Cells[ent.listAtrib.Count+1].Value = dirSigReg;
                r = 0;


                //ent.listReg.Add(new Registro(dirRegAct, dirSigReg));

                if (bandSigReg)
                {
                    ent.archivoDat.Seek(dirSigReg, SeekOrigin.Begin);
                    r = 0;
                }

                r = 0;
                conRen++;
                

            }
            //

           

            ent.archivoDat.Close();
            //RegisInserdataGridView.Sort(RegisInserdataGridView.Columns[posAtrBus], ListSortDirection.Ascending);
        }

        private void RegisInserdataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (bandModi)
            {
                for (int i = 0; i < entAct.listAtrib.Count; i++)
                {
                    RegistroRellDataGrid.Rows[0].Cells[i].Value = RegisInserdataGridView.CurrentRow.Cells[i+1].Value;
                }
            }

            bandModi = false;
        }

        private void EliminarReg_Click(object sender, EventArgs e)
        {
            bandElim = true;
        }

        

        private void RegisInserdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)//Elimina Registro
        {
            int indFilEli;

            if (bandElim == true)
            {
                //MessageBox.Show("Voy a cambiar el valor de " + RegisInserdataGridView.Rows[algo - 1].Cells[entAct.listAtrib.Count + 1].Value+ " por "+RegisInserdataGridView.Rows[algo+1].Cells[0].Value);
                //MessageBox.Show("Vas a eliminar el renglon " + algo+" valor: "+ RegisInserdataGridView.Rows[algo].Cells[1].Value);

                indFilEli = RegisInserdataGridView.CurrentRow.Index;

                //Prueba de direccion 
                //int dir = entAct.leerDatoReg((int)RegisInserdataGridView.Rows[indFilEli - 1].Cells[entAct.listAtrib.Count + 1].Value - 8);
                r = 0;

                using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.nombre + ".dat", FileMode.Open)))
                {
                    r = 0;

                    if ((int)RegisInserdataGridView.Rows[indFilEli].Cells[0].Value  == entAct.dirDat)//Es el primer registro
                    {
                        entAct.dirDat = (int)RegisInserdataGridView.Rows[indFilEli + 1].Cells[0].Value;
                        using (BinaryWriter bwD = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                        {
                            bwD.Seek((int)entAct.dirEnti + 51, SeekOrigin.Begin);
                            bwD.Write(entAct.dirDat);
                        }
                            

                    }
                    else
                    if ((int)RegisInserdataGridView.Rows[indFilEli].Cells[entAct.listAtrib.Count + 1].Value == -1)//Es el ultimo registro
                    {
                        r = 0;
                        int algo = (int)RegisInserdataGridView.Rows[indFilEli - 1].Cells[entAct.listAtrib.Count + 1].Value-8;
                        r = 0;


                        bw.Seek((int)RegisInserdataGridView.Rows[indFilEli - 1].Cells[entAct.listAtrib.Count + 1].Value-8, SeekOrigin.Begin);
                        bw.Write(-1);
                    }
                    else//Es cualquier registro de en medio
                    {
                        r = 0;
                        bw.Seek((int)RegisInserdataGridView.Rows[indFilEli - 1].Cells[entAct.listAtrib.Count + 1].Value - 8, SeekOrigin.Begin);
                        bw.Write((int)RegisInserdataGridView.Rows[indFilEli + 1].Cells[0].Value);
                    }
                                        
                    r = 0;
                }
      
                limpiaGridInsertadosReg();

                bandElim = false;
            }
            
            
            r = 0;
        }

        private void Datos_FormClosing(object sender, FormClosingEventArgs e)
        {
            lonRegisAct = 0;
            AplicaCambio.Visible = false;
            limpiaGridRellReg();
            limpiaGridInsertadosReg();
            comboBoxEntiDatos.Items.Clear();
        }

        private void ModificaRegistro_Click(object sender, EventArgs e)
        {
            

            bandModi = true;
            MessageBox.Show("Selecciona el renglon a modificar");
            AplicaCambio.Visible = true;

        }

        private void AplicaCambio_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < entAct.listAtrib.Count;i++)
            {
                RegisInserdataGridView.CurrentRow.Cells[i + 1].Value = RegistroRellDataGrid.Rows[0].Cells[i].Value;
            }
            int posI = 0;
            int dir = 0;
            string posS = "";
            int contChar = 0;
            //RegisInserdataGridView.Sort(RegisInserdataGridView.Columns[posAtrBus], ListSortDirection.Ascending);

            using (BinaryWriter bw = new BinaryWriter(File.Open(entAct.nombre + ".dat", FileMode.Open)))
            {
                // bw.Seek(, SeekOrigin.Begin);
                
                int longAcumReg = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[0].Value.ToString()) + 8;
                bw.Seek(longAcumReg, SeekOrigin.Begin);

                r = 0;
                for (int i = 0; i < entAct.listAtrib.Count; i++)
                {
                    if (entAct.listAtrib.ElementAt(i).tipo == 'C')
                    {
                        
                        //posS = RegisInserdataGridView.CurrentRow.Cells[i + 1].Value.ToString();                 
                        r = 0;
                        bw.Write(RegisInserdataGridView.CurrentRow.Cells[i + 1].Value.ToString());
                        while (contChar < entAct.listAtrib.ElementAt(i).longitud - RegisInserdataGridView.CurrentRow.Cells[i + 1].Value.ToString().Length)
                        {
                            bw.Write('-');
                            contChar++;
                            r = 0;
                        }
                        r = 0;
                        contChar = 0;
                        longAcumReg += entAct.listAtrib.ElementAt(i).longitud;
                        bw.Seek(longAcumReg, SeekOrigin.Begin);
                        //dir = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[0].Value.ToString()) + entAct.listAtrib.ElementAt(i).longitud;
                    }
                    else
                    {
                        posI = Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[i + 1].Value.ToString());
                        r = 0;
                        bw.Write(Int32.Parse(RegisInserdataGridView.CurrentRow.Cells[i + 1].Value.ToString()));

                        longAcumReg += 4;
                        r = 0;
                        bw.Seek(longAcumReg, SeekOrigin.Begin);
                    }
                    
                    r = 0;
                    //bw.Write((int)RegisInserdataGridView.CurrentRow.Cells[i+1].Value);
                    //bw.Seek((int)RegisInserdataGridView.CurrentRow.Cells[i].Value+entAct.listAtrib.ElementAt(i).longitud,SeekOrigin.Begin);

                    //RegisInserdataGridView.CurrentRow.Cells[i + 1].Value = RegistroRellDataGrid.Rows[0].Cells[i].Value;
                }
                longAcumReg = 0;
                //RegisInserdataGridView.CurrentRow.Cells[i + 1].Value;

            }
        }

        private void RegisInserdataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            r = 0;
            
        }
    }
}
