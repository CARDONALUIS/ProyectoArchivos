﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;


namespace PruebaProyecto
{

    /*
     * Clase que se encarga de realizar las acciones que se encomiendan hacer en el hashEstaticos
     * */
    class HashEstatico
    {
        public FileStream archivHash;
        public int archDirDatHash;
        public int valorModu = 7;
        public CajonHash[] DirectorioHash;
        public Entidad entAct;
        public Diccionario dic;
        int r = 0;



        //Metodo que se encarga de poner en nulo los valores de un bloque Hash de cierta direccion y con cierto tamaño en el archivo
        public void restablecerBloqueArchivo(int tama, int dir)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(archivHash.Name, FileMode.Open)))
            {
                bw.Seek(dir, SeekOrigin.Begin);
                Byte[] bloque = new Byte[tama];
                for (int i = 0; i < tama; i++)
                {
                    bloque[i] = 0xFF;
                }
                bw.Write(bloque);


            }
        }


        //Este metodo actualiza un cajon hash despues de haber desarrollado un cambio
        public void actualizaListaHashCajonArchivo(int numCajon, long dirCajon)
        {
            //if (dirCajon != -1)
            //{
                restablecerBloqueArchivo(512, (int)dirCajon);
                r = 0;

                using (BinaryWriter bw = new BinaryWriter(File.Open(archivHash.Name, FileMode.Open)))
                {
                    bw.Seek((int)dirCajon, SeekOrigin.Begin);
                    foreach (campoCajonHash b in DirectorioHash[numCajon].listaCampoCajonHash)
                    {
                        bw.Write(b.clave);
                        bw.Write(b.apunReg);
                    }
                }
                r = 0;
            //}

        }

        //Actualiza el valor a donde apunta cierto cajon y lo inisializa desde con valores nulo la direccion que le corresponde en el cajon
        public void actualizaDirectorioArchivo(int numCajon, long valor)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(archivHash.Name, FileMode.Open)))
            {
                bw.Seek(numCajon * 8, SeekOrigin.Begin);
                r = 0;
                bw.Write(valor);

                bw.Seek((int)valor, SeekOrigin.Begin);
                Byte[] bloque = new Byte[512];
                for (int i = 0; i < 512; i++)
                {
                    bloque[i] = 0xFF;
                }
                bw.Write(bloque);


            }

        }


        //Este metodo realiza la insersion de una clave y su direccion en su correspondiente cajon de manera logica
        public void insertaEnCajon(int cajon, int clave,long dirReg)
        {
            
            r = 0;
            for(int i = 0; i < DirectorioHash.Length; i++)
            {
                if (i == cajon)
                {
                    r = 0;
                    campoCajonHash camHas = new campoCajonHash();
                    camHas.apunReg = dirReg;
                    camHas.clave = clave;
                    DirectorioHash[i].listaCampoCajonHash.Add(camHas);


                    List<campoCajonHash> lisOrd = DirectorioHash[i].listaCampoCajonHash;

                    lisOrd = lisOrd.OrderBy(o => o.clave).ToList();

                    DirectorioHash[i].listaCampoCajonHash = lisOrd;


                    long valoCajon;
                    r = 0;
                    if (DirectorioHash[i].dirCajon == 0)
                    {
                        r = 0;
                        archivHash.Close();
                        archivHash = File.Open(archivHash.Name, FileMode.Open);
                        valoCajon = archivHash.Length;
                        DirectorioHash[i].dirCajon = (int)archivHash.Length;
                        archivHash.Close();
                        r = 0;
                        actualizaDirectorioArchivo(i, valoCajon);

                    }
                    else
                        valoCajon = DirectorioHash[i].dirCajon;

                    r = 0;
                    actualizaListaHashCajonArchivo(i, valoCajon);
                }
            }



            r = 0;
        }


        //Metodo seter para tener en la clase los variable entidad y la variable diccionario
        public void setEntYDic(Entidad _ent, Diccionario _dic)
        {
            entAct = _ent;
            dic = _dic;
        }


        //Metodo para obtener el ultimo registro que se ingreso con su clave hash y su direccion para la insercion que corresponde
        public void ObtengRegistro()
        {
            foreach (Atributo a in entAct.listAtrib)
            {
                if (a.dirIndi == -1 && a.tipoIndi == 5)
                {
                    r = 0;
                    using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                    {
                        bw.Seek((int)a.dirAtri + 52, SeekOrigin.Begin);
                        a.dirIndi = 0;
                        bw.Write(0);

                    }
                }
                else
                    r = 0;
            }
            r = 0;


            //creaNodoArb();

            r = 0;
            entAct.archivoDat.Close();
            entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(entAct.archivoDat);
            long dirUltReg;
            int clavAct;

            r = 0;
            dirUltReg = entAct.archivoDat.Length - entAct.longAtributos;
            r = 0;
            entAct.archivoDat.Seek(dirUltReg + archDirDatHash, SeekOrigin.Begin);

            clavAct = br.ReadInt32();

            r = 0;
            entAct.archivoDat.Close();

            archivHash.Close();

            int cajon;

            cajon = obtenCajonModulo(clavAct);

            r = 0;
            insertaEnCajon(cajon, clavAct, dirUltReg);

        }


        //Este metodo obtiene el valor hash de la clave que resibe como parametro
        public int obtenCajonModulo(int clave)
        {
            double modulo = clave % valorModu;
            
            return (int)modulo;
        }


        //Metodo que se encarga de realizar la eliminar toda la informacion de un cajon pero en forma fisica que es dentro del archivo
        public void actualizaEliminaArchivoCajon(int cajonEli)
        {
            int dirElimCajo = DirectorioHash[cajonEli].dirCajon;
            r = 0;

            using (BinaryWriter bw = new BinaryWriter(File.Open(archivHash.Name, FileMode.Open)))
            {
                bw.Seek(dirElimCajo, SeekOrigin.Begin);
                Byte[] bloque = new Byte[12];
                for (int j = 0; j < 12; j++)
                {
                    bloque[j] = 0xFF;
                    bw.Write(bloque[j]);
                }

                foreach (campoCajonHash a in DirectorioHash[cajonEli].listaCampoCajonHash )
                {
                    bw.Write(bloque);
                }

                bw.Seek(dirElimCajo, SeekOrigin.Begin);
                foreach (campoCajonHash a in DirectorioHash[cajonEli].listaCampoCajonHash)
                {
                    bw.Write(a.clave);
                    bw.Write(a.apunReg);
                }
            }

            r = 0;
        }

        //Metodo que establece todos lo metodos para la eliminacion, este metodo de forma general elimina todo lo que implica eliminar una clave
        public void elimina(int clave)
        {
            int cajon = obtenCajonModulo(clave);

            int indElim = DirectorioHash[cajon].listaCampoCajonHash.FindIndex(x => x.clave == clave);
            DirectorioHash[cajon].listaCampoCajonHash.RemoveAt(indElim);

            actualizaEliminaArchivoCajon(cajon);

            if (DirectorioHash[cajon].listaCampoCajonHash.Count == 0)
            {
                r = 0;
                

                using (BinaryWriter bw = new BinaryWriter(File.Open(archivHash.Name, FileMode.Open)))
                {
                    bw.Seek(8 * cajon, SeekOrigin.Begin);
                    Byte[] bloque = new Byte[8];
                    for (int i = 0; i < 8; i++)
                    {
                        bloque[i] = 0xFF;
                    }
                    bw.Write(bloque);

                }

                DirectorioHash[cajon] = new CajonHash();
            }
            r = 0;
        }


        //Este metodo inizialisa el directorio de cajones
        public void creaDirectorioHash()
        {
            BinaryWriter bw = new BinaryWriter(archivHash);
                Byte[] bloque = new Byte[valorModu * 8];
                for (int i = 0; i < valorModu * 8; i++)
                {
                    bloque[i] = 0xFF;
                }
                bw.Write(bloque);
            DirectorioHash = new CajonHash[valorModu];

            for (int i = 0; i < valorModu; i++)
            {
                DirectorioHash[i] = new CajonHash();
            }
            r = 0;
            archivHash.Close();
        }


        //Este metodo actualiza de forma logica que es en memoria el directorio actual
        public void actualizaDirectorioLogico()
        {
            r = 0;
            
            int contCajon = 0;
            DirectorioHash = new CajonHash[valorModu];

            for (int i = 0; i < valorModu; i++)
            {
                DirectorioHash[i] = new CajonHash();
            }

            while (contCajon < valorModu)
            {
                archivHash.Close();
                archivHash = File.Open(archivHash.Name, FileMode.Open);
                BinaryReader br = new BinaryReader(archivHash);

                archivHash.Seek(contCajon * 8, SeekOrigin.Begin);

                long valorDirCajon = br.ReadInt64();
                if(contCajon == 3)
                r = 0;
                if (valorDirCajon != -1)
                {
                    r = 0;
                    DirectorioHash[contCajon] = new CajonHash();
                    DirectorioHash[contCajon].dirCajon = (int)valorDirCajon;
                    actualizaListaHash((int)valorDirCajon, contCajon);
                    r = 0;
                }    
                contCajon ++;
            }

            archivHash.Close();

            r = 0;
        }


        //Metodo que actualiza de forma logica la lista de claves y directorio hash 
        public void actualizaListaHash(int dirCajon, int numCajon)
        {
            archivHash.Close();
            archivHash = File.Open(archivHash.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(archivHash);
            archivHash.Seek(dirCajon, SeekOrigin.Begin);

            r = 0;
            int clvComp = br.ReadInt32();
            long dirReg = br.ReadInt64();

            r = 0;
            while(clvComp != -1)
            {
                campoCajonHash camHas = new campoCajonHash();
                camHas.clave = clvComp;
                camHas.apunReg = dirReg;

                r = 0;
                DirectorioHash[numCajon].listaCampoCajonHash.Add(camHas);
            
                clvComp = br.ReadInt32();
                dirReg = br.ReadInt64();
                r = 0;
            }
            r = 0;
            archivHash.Close();
        }


        //Metodo que ayuda a verificar si hay claves que ya estan contenidas en los cajon y se quiera insertar una repetida
        public bool hayClaveRepetida(int clave)
        {
            int cajon = clave % valorModu;
            campoCajonHash prueb;


            if (DirectorioHash[cajon].listaCampoCajonHash.Count != 0)
                prueb = DirectorioHash[cajon].listaCampoCajonHash.Find(x => x.clave == clave);
            else
                return false;


            if (prueb != null)
                return true;
            else
                return false;

        }

        //Metodo constructor que inisializa variables y archivo para la clase
        public HashEstatico(FileStream _arcHash, int _archDirHash, bool tieneDatos)
        {
            archivHash = _arcHash;
            archDirDatHash = _archDirHash;


            if(tieneDatos)
            {
                _arcHash.Close();
                archivHash = File.Open(_arcHash.Name, FileMode.Open);
                
                actualizaDirectorioLogico();
                //actualizaListaHash();
                archivHash.Close();
            }
            else
            {
                r = 0;
                creaDirectorioHash();
            }
        }
    }
}
