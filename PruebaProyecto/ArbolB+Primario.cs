﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;



namespace PruebaProyecto
{
    /*
    Clase que contiene todo el codigo relacionado a la creacion del arbol primario 
     */
    class ArbolB_Primario
    {
        int r = 0;
        public FileStream archArb;
        public int dirArchDatArb;
        public Entidad entAct;
        public Diccionario dic;
        public List<Nodo> lisNodo  = new List<Nodo>();
        public Nodo hojaActual_L;
        public int n = 5;
        public bool bandT = false;
        public List<Nodo> lisNodPad;
        public int kPriBorrar;


        //Metodo para detectar que cuando llegue al punto de tener solo un nodo padre 
        public bool esraizOrigen(Nodo N)
        {
            int contValo = 0;
            bool bandOrigen = true;

            if (N.tipo == 'R')
            {
                if(N.K.Count != N.P.Count)
                {
                    bandOrigen = false;
                }
            }
            else
                bandOrigen = false;

            r = 0;

            return bandOrigen;
        }

        //Metodo para actualizar valores de un nodo cuando se eliminan valores en un nodo del archivo  
        public void actualizaEliArchNodo(Nodo N)
        {
            r = 0;
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {
                bw.Seek((int)N.dirNodo, SeekOrigin.Begin);
                Byte[] bloque = new Byte[57];
                Byte[] bloqueP = new Byte[65];


                if (N.tipo == 'H')
                {
                    for (int j = 0; j < 57; j++)
                    {
                        bloque[j] = 0xFF;
                        bw.Write(bloque[j]);
                    }
                }
                else
                {
                    for (int k = 0; k < 65; k++)
                    {
                        bloqueP[k] = 0xFF;
                        bw.Write(bloqueP[k]);
                    }
                }

                bw.Seek((int)N.dirNodo, SeekOrigin.Begin);
                bw.Write(N.dirNodo);
                bw.Write(N.tipo);
                int i = 0;
                for(; i < N.K.Count;i++)
                {
                    bw.Write(N.P.ElementAt(i));
                    bw.Write(N.K.ElementAt(i));
                }

                if(N.tipo != 'H' && N.K.Count != 0 && !esraizOrigen(N))
                {
                    r = 0;
                    bw.Write(N.P.ElementAt(i));
                }

                if (N.tipo == 'H' && N.P.Count == n)
                    bw.Write(N.P.ElementAt(N.P.Count - 1));
            }
        }


        //Metodo que encuentra primero el nodo al que pertenece la clave a borrar y despues ejecuta el metodo que lo elimina de forma general 
        public void borrar(int K, long P)
        {
            r = 0;
            Nodo L = encuentraNodoHoja(K);
            borrar_entrada(L, K, P);
        }

        //Borra los valores especificados del nodo especificado
        public void borrarValoresSelec(Nodo L, int K , long P)
        {
            r = 0;
            int indexK = L.K.FindIndex(x => x == K);
            int indexP = L.P.FindIndex(x => x == P);
            L.K.RemoveAt(indexK);
            L.P.RemoveAt(indexP);
            r = 0;
        }


        //Metodo que ayuda a seleccionar que nodo pedir prestado para una posible eliminacion
        public Nodo seleccionaHermano(Nodo N, int K, long P)
        {
            Nodo pad = buscaPadre(N);
            Nodo hermano = null;

            if(N.tipo != 'H')
            r = 0;
            //int index = pad.K.FindIndex(x => x >= K && x < K);
            r = 0;
            for(int i = 0; i < pad.K.Count;i++)
            {
                r = 0;
                if (i == 0 && K < pad.K.ElementAt(i) && N.tipo == 'H')//Toma el de la derecha
                {
                    r = 0;
                    kPriBorrar = pad.K.ElementAt(i);
                    hermano = lisNodo.Find(x => x.dirNodo == pad.P.ElementAt(i+1));
                    r = 0;
                    break;
                }
                else
                if (i == pad.K.Count - 1 && K >= pad.K.ElementAt(i) && N.tipo == 'H')//Toma el de la izquierda//PROFE
                {
                    r = 0;
                    kPriBorrar = pad.K.ElementAt(i);
                    r = 0;                   
                    hermano = lisNodo.Find(x => x.dirNodo == pad.P.ElementAt(pad.P.Count-2));
                    r = 0;
                    break;
                }
                else
                if (pad.tipo == 'R' && pad.K.Count == 1 && pad.P.Count == 2)//Es el ultimo nodo raiz
                {
                    r = 0;
                    kPriBorrar = pad.K.ElementAt(i);
                    if (K >= pad.K.ElementAt(i))
                        hermano = lisNodo.Find(x => x.dirNodo == pad.P.ElementAt(i));
                    else
                        hermano = lisNodo.Find(x => x.dirNodo == pad.P.ElementAt(i + 1));
                    r = 0;
                }
                else
                {
                    r = 0;
                    if (K >= pad.K.ElementAt(i) && K < pad.K.ElementAt(i + 1))//MIO
                    {
                        r = 0;
                        kPriBorrar = pad.K.ElementAt(i + 1);
                        hermano = lisNodo.Find(x => x.dirNodo == pad.P.ElementAt(i + 2));
                        r = 0;
                        if (hermano.K.Count <= n / 2)
                        {
                            r = 0;
                            if ((lisNodo.Find(x => x.dirNodo == pad.P.ElementAt(i)).K.Count > 2))
                            {
                                kPriBorrar = pad.K.ElementAt(i);
                                r = 0;
                                hermano = lisNodo.Find(x => x.dirNodo == pad.P.ElementAt(i));
                                r = 0;
                            }
                            else
                                r = 0;

                        }

                        r = 0;
                        break;
                    }
                    r = 0;
                }

            }
            r = 0;
            return hermano;
            
        }


        //Metodo que se encarga de acomodar valores entre dos nodos en base a la bandera espsificada
        public void concatenaPares(Nodo N, Nodo NPri, bool concatenarPrincipio)
        {
            if (concatenarPrincipio)
            {
                for(int i = N.K.Count-1; i>= 0;i--)
                {
                    NPri.K.Insert(0, N.K.ElementAt(i));
                }
                for (int i = N.P.Count - 1; i >= 0; i--)
                {
                    NPri.P.Insert(0, N.P.ElementAt(i));
                }
            }
            else
            {
                for (int i = 0; i < N.K.Count; i++)
                {
                    NPri.K.Add(N.K.ElementAt(i));
                }
                for (int j = 0; j < N.P.Count; j++)
                {
                    NPri.P.Add(N.P.ElementAt(j));
                }
            }
            r = 0;
            archArb.Close();
            archArb = File.Open(archArb.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(archArb);
            archArb.Seek((int)N.dirNodo + 57, SeekOrigin.Begin);
            long apSigN = br.ReadInt64();
            archArb.Close();
            r = 0;
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {
                bw.Seek((int)NPri.dirNodo + 57, SeekOrigin.Begin);
                bw.Write(apSigN);
            }

            r = 0;
        }


        //Metodo que se encarga de cambiar todos las claves y apuntadores entre dos nodos exepto los apuntadores de esos nodos
        public void intercambiar_variables(Nodo N, Nodo Npri)
        {
            r = 0;
            long dirAux;
            dirAux = N.dirNodo;            
            //Primeros apuntadores
            N.dirNodo = Npri.dirNodo;
            Npri.dirNodo = dirAux;

        }


        //Este metodo borra un nodo de forma fisica en el archivo
        public void borraNodo(Nodo N)
        {
            N.K.Clear();
            N.P.Clear();

            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {
               
                Byte[] bloque = new Byte[56];//Elimina todo menos direccion y tipo en el archivo
                bw.Seek((int)N.dirNodo + 9, SeekOrigin.Begin);
                for (int j = 0; j < 56; j++)
                {
                    bloque[j] = 0xFF;
                    bw.Write(bloque[j]);
                }

            }
            r = 0;
         }



        //Metodo que se encarga de concatenar valores entre un nodo y otro espesificando una bander pero solo es para nodos que no son hojas
        public void concatenarNoHoja(int K, Nodo N, Nodo Npri, bool concatenarAlPrincipio)
        {
            if (!concatenarAlPrincipio)
            {
                Npri.K.Add(K);
                foreach (int a in N.K)
                {
                    Npri.K.Add(a);
                }
                foreach (long b in N.P)
                {
                    Npri.P.Add(b);
                }
            }
            else
            {
                r = 0;
                Npri.K.Insert(0, K);
                r = 0;
                for (int i = N.K.Count - 1; i >= 0; i--)
                {
                    Npri.K.Insert(0, N.K.ElementAt(i));
                }
                for (int i = N.P.Count - 1; i >= 0; i--)
                {
                    Npri.P.Insert(0, N.P.ElementAt(i));
                }
                r = 0;

            }
            r = 0;
        }

        /*
        Nodo que se encarga de forma general de borrar en el nodo y llamar 
        el pertinente metodo dadas las condiciones del nodo una vez sido borrado cabe mensionar que este
        metoodo es recursivo 
        */
        public void borrar_entrada(Nodo N, int K, long P)
        {
            r = 0;
            borrarValoresSelec(N, K, P);
            r = 0;

            if(N.tipo == 'R' && N.P.Count == 1)//Esta asignacion no esta muy entendible
            {
                r = 0;
                Nodo nueRaiz = lisNodo.Find(x => x.dirNodo == N.P.ElementAt(0));
                nueRaiz.tipo = 'R';
                N.P.Clear();
                r = 0;
                actualizaEliArchNodo(N);
                actualizaEliArchNodo(nueRaiz);
            }
            else
            {
                r = 0;
                if (N.K.Count < n / 2 && N.tipo != 'R')//N tiene muy poco valores/punteros
                {
                    Nodo Npri = seleccionaHermano(N, K, P);//Encuentra un hermano 
                    int kPri = kPriBorrar;
                    bool concatenarPrincipio = false;
                    r = 0;
                    if (N.K.Count + Npri.K.Count <= n - 2)//las entradas en N y N' caben en un solo nodo  FUSIONAR LOS NODOS
                    {
                        r = 0;
                        if (N.K.Max() < Npri.K.Min())//N es predecesor de Npri
                        {
                            r = 0;
                            intercambiar_variables(N, Npri);
                            concatenarPrincipio = true;
                            r = 0;                           
                        }
                        r = 0;
                        if (N.tipo != 'H')//No es una nodo hoja;
                        {
                            r = 0;
                            concatenarNoHoja(kPri, N, Npri, concatenarPrincipio);
                            r = 0;                           
                        }
                        else//Concatenar todos los pares(ki, pi) en N a N'
                        {
                            r = 0;
                            concatenaPares(N, Npri, concatenarPrincipio);
                        }
                        r = 0;
                        borrar_entrada(buscaPadre(N), kPri, N.dirNodo);
                        r = 0;
                        borraNodo(N);
                        actualizaEliArchNodo(Npri);
                        
                    }
                    else//REDISTRIBUCION: TOMAR PRESTADA UNA ENTRADA DE N'
                    {
                        r = 0;
                        int mK;
                        long mP;

                        if (Npri.K.Max() < N.K.Min())//Npri es predecesor de N
                        {
                            r = 0;
                            if (N.tipo == 'I')
                            {
                                r = 0; ///////////las///////////////////////////////////////////////////////////////////
                                mP = Npri.P.Last();
                                mK = Npri.K.Last();
                                r = 0;
                                Npri.K.RemoveAt(Npri.K.Count - 1);
                                Npri.P.RemoveAt(Npri.P.Count - 1);
                                N.P.Insert(0, mP);
                                N.K.Insert(0, kPri);
                                r = 0;
                                Nodo padre = buscaPadre(N);
                                int indKPri = padre.K.FindIndex(x => x == kPri);
                                padre.K.RemoveAt(indKPri);
                                padre.K.Insert(indKPri, mK);
                                r = 0;
                                actualizaEliArchNodo(N);
                                actualizaEliArchNodo(Npri);
                                actualizaEliArchNodo(padre);
                                r = 0;
                            }
                            else
                            {
                                r = 0;
                                mK = Npri.K.Last();
                                mP = Npri.P.Last();
                                r = 0;
                                Npri.K.RemoveAt(Npri.K.Count - 1);
                                Npri.P.RemoveAt(Npri.P.Count - 1);
                                r = 0;
                                N.K.Insert(0, mK);
                                N.P.Insert(0, mP);
                                r = 0;
                                Nodo padre = buscaPadre(N);
                                r = 0;
                                int indKPri = padre.K.FindIndex(x => x == kPri);
                                padre.K.RemoveAt(indKPri);
                                padre.K.Insert(indKPri, mK);
                                r = 0;
                                actualizaEliArchNodo(N);
                                actualizaEliArchNodo(Npri);
                                actualizaEliArchNodo(padre);                               
                            }
                        }
                        else//Npri es sucesor de N
                        {
                            r = 0;
                            if (N.tipo == 'I')
                            {
                                r = 0;//////////////////////////////////////////////////////////////////////////
                                mP = Npri.P.ElementAt(0);
                                mK = Npri.K.ElementAt(0);
                                r = 0;
                                Npri.K.RemoveAt(0);
                                Npri.P.RemoveAt(0);
                                r = 0;
                                N.P.Add(mP);
                                N.K.Add(kPri);
                                r = 0;
                                Nodo padre = buscaPadre(N);
                                int indKPri = padre.K.FindIndex(x => x == kPri);
                                padre.K.RemoveAt(indKPri);
                                padre.K.Insert(indKPri, mK);
                                r = 0;
                                actualizaEliArchNodo(N);
                                actualizaEliArchNodo(Npri);
                                actualizaEliArchNodo(padre);
                                r = 0;
                            }
                            else
                            {
                                mK = Npri.K.ElementAt(0);
                                mP = Npri.P.ElementAt(0);
                                r = 0;
                                Npri.K.RemoveAt(0);
                                Npri.P.RemoveAt(0);
                                r = 0;
                                N.K.Add(mK);
                                N.P.Add(mP);
                                r = 0;
                                mK = Npri.K.ElementAt(0);
                                mP = Npri.P.ElementAt(0);
                                r = 0;
                                Nodo padre = buscaPadre(N);
                                int indKPri = padre.K.FindIndex(x => x == kPri);
                                r = 0;
                                padre.K.RemoveAt(indKPri);
                                padre.K.Insert(indKPri, mK);
                                r = 0;
                                actualizaEliArchNodo(N);
                                actualizaEliArchNodo(Npri);
                                actualizaEliArchNodo(padre);
                            }
                        }
                    }
                }
                else
                {
                    r = 0;
                    actualizaEliArchNodo(N);
                    r = 0;
                }

            }

        }

        /*
        DE AQUI PARA ARRIBA SON METODOS DE ELIMINACION//////////////////////////////////////////////////////////////////////////////////////////////////////
        */


        //Metodo que se encarga de crear un espacio en el archivo que represente el tamaño de un nodo
        public void creaEspNodo(char tipo)
        {
            r = 0;
            archArb.Close();
            archArb = File.Open(archArb.Name, FileMode.Open);
            long finArchivo = archArb.Length;
            archArb.Close();
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {
                bw.Seek((int)finArchivo, SeekOrigin.Begin);

                Byte[] bloque = new Byte[57];
                for (int i = 0; i < 57; i++)
                {
                    bloque[i] = 0xFF;
                }
                r = 0;
                bw.Write(bloque);
                if(tipo == 'H')
                {
                    bw.Write((long)-1);
                }
                else
                {
                    Byte[] bloque2 = new Byte[8];
                    for (int i = 0; i < 8; i++)
                    {
                        bloque2[i] = 0xFF;
                    }
                    bw.Write(bloque2);
                }
            }
        }
        

        //Metodo que se encarga de leer el archivo para actualizar de manera logica la lista de nodos
        public void actualizaListaNodo()
        {
            BinaryReader br = new BinaryReader(archArb);
            long dicNod;
            char[] Tipo;
            long dirPun;
            int clv;
            bool bandFinClv = true;
            r = 0;
            int posAct = 0;
            int posIni = 0;
            bool masNodos = true;
            while (true)
            {
                archArb.Seek((int)posIni, SeekOrigin.Begin);
                posIni = posAct;
                r = 0;
                Nodo no = new Nodo();
                dicNod = br.ReadInt64();
                Tipo = br.ReadChars(1);
                no.dirNodo = dicNod;
                no.tipo = Tipo.ElementAt(0);
                posAct += 9;
                r = 0;
                while (br.ReadInt64() != -1 && no.K.Count < n-1)
                {
                    archArb.Seek(posAct, SeekOrigin.Begin);
                    dirPun = br.ReadInt64();
                    r = 0;
                    if (no.tipo == 'H' || no.dirNodo == 0)
                    {
                        clv = br.ReadInt32();
                        no.K.Add(clv);
                        no.P.Add(dirPun);
                        posAct += 12;
                        r = 0;
                    }
                    else
                    {
                        r = 0;
                        clv = br.ReadInt32();
                        r = 0;
                        if (clv!= -1)
                        no.K.Add(clv);
                        no.P.Add(dirPun);
                        posAct += 12;
                        r = 0;
                    }
                    r = 0;
                    if (br.ReadInt64() == -1)//No hay mas claves
                    {
                        bandFinClv = false;
                        r = 0;
                    }
                    r = 0;
                    archArb.Seek(posAct, SeekOrigin.Begin);
                }
                r = 0;             
                if(no.P.Count == n-1 && no.tipo == 'R' && no.dirNodo == 0)
                {
                    r = 0;
                    archArb.Seek(posIni + 57, SeekOrigin.Begin);
                    long val = br.ReadInt64();
                    r = 0;                    
                    no.P.Add(val);
                }

                if(no.P.Count == n-1 && no.tipo == 'H')
                {
                    r = 0;
                    archArb.Seek(posIni + 57, SeekOrigin.Begin);
                    no.P.Add(br.ReadInt64());
                    r = 0;
                }

                if(no.P.Count == n-1  && no.tipo != 'H')
                {
                    r = 0;
                    archArb.Seek(posIni + 57, SeekOrigin.Begin);
                    long val = br.ReadInt64();
                    if(val!= -1)
                    no.P.Add(val);
                    r = 0;
                }


                if (posIni + 65 == archArb.Length)
                {
                    masNodos = false;

                    r = 0;
                }

                r = 0;
               
                if (!masNodos)//llego al final y ya no tiene mas campos que agregar
                {

                    lisNodo.Add(no);
                    masNodos = true;
                    break;

                }
                else
                {

                    lisNodo.Add(no);
                    posIni += 65;
                }             
                posAct = posIni;                
            }
            r = 0;
        }
        

        //Constructor de la clase para establecer ciertos valores de la clase
        public ArbolB_Primario(FileStream _archArb, bool recInf, int _dirArchDatArb)//El archivo, y si lo abrieron con informacion ya almacenada
        {
            archArb = _archArb;
            dirArchDatArb = _dirArchDatArb;

            if (recInf)
            {
                _archArb.Close();
                archArb = File.Open(_archArb.Name, FileMode.Open);
                actualizaListaNodo();
                archArb.Close();              
            }
            else
            {
                
            }
        }


        //Metodo seter de la entidad y el diccionario para tener esos valores en esta clase
        public void setEntYDic(Entidad _ent, Diccionario _dic)
        {
            entAct = _ent;
            dic = _dic;            
            r = 0;
        }

        //Obtiene el valor de la clave primaria y su direccion del ultimo registro que se guardo
        public void ObtengRegistro()
        {
            
            foreach (Atributo a in entAct.listAtrib)
            {
                if (a.dirIndi == -1 && a.tipoIndi == 4)
                {
                    r = 0;
                    using (BinaryWriter bw = new BinaryWriter(File.Open(dic.nomArchivo, FileMode.Open)))
                    {
                        bw.Seek((int)a.dirAtri + 52, SeekOrigin.Begin);
                        a.dirIndi = 0;
                        bw.Write(0);

                    }
                    creaEspNodo('H');

                }
                else
                    r = 0;
            }

            entAct.archivoDat.Close();
            entAct.archivoDat = File.Open(entAct.archivoDat.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(entAct.archivoDat);
            long dirUltReg;
            int clavAct;

            r = 0;
            dirUltReg = entAct.archivoDat.Length - entAct.longAtributos;
            entAct.archivoDat.Seek(dirUltReg + dirArchDatArb,SeekOrigin.Begin);

            clavAct = br.ReadInt32();

            r = 0;
            entAct.archivoDat.Close();

            
            archArb.Close();
            inserta(clavAct, dirUltReg);

        }


        /*
         * Metodo que de forma general inserta la clave primaria y su apuntador
         * y llamando a los metodos pertinentes dado el caso de la tipo de insericion que debe de ser
         */
        public void inserta(int K, long P)
        {
            r = 0;
          if(lisNodo.Count == 0)
            {
                r = 0;
                Nodo hoja = new Nodo();
                hoja.tipo = 'R';
                //hoja.dirSigNod = -1;
                hoja.dirNodo = P;
                lisNodo.Add(hoja);
                hojaActual_L = hoja;
                r = 0;
            }
          else//Encontrar el nodo hoja en  que debera contener el valor de llave K
            {
                r = 0;

                hojaActual_L = encuentraNodoHoja(K);
                
                r = 0;

            }

            r = 0;
            if(hojaActual_L.K.Count() < n-1 )
            {
                r = 0;
               inset_in_leaf(hojaActual_L, K, P);
            }
            else//Dividir el nodo
            {
                r = 0;
                //Crear Nodo LPrima
                Nodo LPri = new Nodo();
                creaNodo(LPri, 'H');
                Nodo T = new Nodo();
                copiaValAT(hojaActual_L, T);
                bandT = true;
                //T = hojaActual_L;

                r = 0;
                inset_in_leaf(T, K, P);
                bandT = false;
                r = 0;
                setPunteroSigHoja(hojaActual_L, LPri);
                r = 0;
                //borrHojaActua(hojaActual_L);
                hojaActual_L.K.Clear();
                hojaActual_L.P.Clear();
                r = 0;
                repartirValoresT(T, hojaActual_L, LPri,'H');
                r = 0;
                actulizarDivisionArchivo(hojaActual_L, LPri);
                r = 0;
                int KPri = LPri.K.Min();
                r = 0;
                insert_in_parent(hojaActual_L, KPri, LPri);
                r = 0;

            }
            r = 0;
            
        }

        //Metodo que actualiza en el archivo los dos nodos que se repartieron valores
        public void actulizarDivisionArchivo(Nodo L, Nodo LPri)
        {
            Byte[] bloque = new Byte[48];
            for (int i = 0; i < 48; i++)
            {
                bloque[i] = 0xFF;
            }

            r = 0;
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {   
                bw.Seek((int)L.dirNodo+9, SeekOrigin.Begin);
                bw.Write(bloque);
                r = 0;

                if (L.tipo == 'H' || L.dirNodo == 0 && L.tipo == 'R')
                {
                    r = 0;
                    bw.Seek((int)L.dirNodo + 9, SeekOrigin.Begin);
                    for (int i = 0; i < L.K.Count; i++)
                    {
                        bw.Write(L.P.ElementAt(i));
                        bw.Write(L.K.ElementAt(i));
                    }
                    r = 0;

                    bw.Seek((int)LPri.dirNodo + 9, SeekOrigin.Begin);
                    for (int i = 0; i < LPri.K.Count; i++)
                    {
                        bw.Write(LPri.P.ElementAt(i));
                        bw.Write(LPri.K.ElementAt(i));
                    }
                }
                else
                {
                    r = 0;

                    bw.Seek((int)L.dirNodo + 9, SeekOrigin.Begin);
                    for (int i = 0; i < L.P.Count; i++)
                    {
                        bw.Write(L.P.ElementAt(i));
                        if (i != L.P.Count-1 )
                        {
                            r = 0;
                            bw.Write(L.K.ElementAt(i));
                        }
                        r = 0;
                    }
                    r = 0;

                    bw.Seek((int)LPri.dirNodo + 9, SeekOrigin.Begin);
                    for (int i = 0; i < LPri.P.Count; i++)
                    {
                        bw.Write(LPri.P.ElementAt(i));
                        if (i != L.P.Count - 1)
                        {
                            r = 0;
                            bw.Write(LPri.K.ElementAt(i));
                        }
                        r = 0;
                    }


                }

                r = 0;
            }
        }

        //Este metodo sirve para guardar los valores que tiene un nodo en otro nodo llamado T para usarlo como respaldo
        public void copiaValAT(Nodo L, Nodo T)
        {
            foreach (int a in L.K)
                T.K.Add(a);
            foreach (int b in L.P)
                T.P.Add(b);
        }


        //Este metodo sirve solo para actualizar valores en el archivo pero del nodo raiz
        public void actualizaNodoRaizArchivo(Nodo n)
        {
            
            r = 0;
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {
                bw.Seek((int)n.dirNodo, SeekOrigin.Begin);
                bw.Write(n.dirNodo);
                bw.Write(n.tipo.ToString().ToCharArray());
                bw.Write(n.P.ElementAt(0));
                bw.Write(n.K.ElementAt(0));
                bw.Write(n.P.ElementAt(1));
                r = 0;
            }
            r = 0;

        }


        /* 
         * Este metodo se manda llamar cuando se tiene que crear un nodo padre , pero tomando en 
         * cuenta los metodo pertinentes dada las condiciones del arbol en ese momento
         */
        public void insert_in_parent(Nodo N,int KPri, Nodo NPri)
        {
            r = 0;
            if(N.tipo == 'R')
            {
                r = 0;
                Nodo R = new Nodo();
                R.dirNodo = NPri.dirNodo + 65;
                R.tipo = 'R';
                R.P.Add(N.dirNodo);
                R.K.Add(KPri);
                R.P.Add(NPri.dirNodo);
                r = 0;
                creaEspNodo(R.tipo);
                actualizaNodoRaizArchivo(R);
                r = 0;
                if(N.dirNodo == 0 && N.tipo == 'R')
                {
                    N.tipo = 'H';
                    r = 0;
                    using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                    {
                        char[] tipoNu = N.tipo.ToString().ToCharArray();
                        bw.Seek((int)N.dirNodo + 8, SeekOrigin.Begin);
                        bw.Write(tipoNu);
                    }
                    r = 0;

                }
                else
                {
                    r = 0;
                    N.tipo = 'I';
                    using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                    {
                        bw.Seek((int)N.dirNodo+8,SeekOrigin.Begin);
                        bw.Write(N.tipo.ToString().ToCharArray());
                        bw.Seek((int)N.dirNodo + 57, SeekOrigin.Begin);
                        bw.Write((long)-1);

                    }
                    r = 0;


                    //actualizo el anterior a un tipo intermedio

                }
                r = 0;
                lisNodo.Add(R);
                r = 0;
                return;
            }

            Nodo Pad = buscaPadre(N); 

            r = 0;
            if (Pad.P.Count < n)
            {
                int index = Pad.P.FindIndex(x => x == N.dirNodo);
                Pad.K.Insert(index, KPri);
                Pad.P.Insert(index + 1, NPri.dirNodo);
                //Pad.K.Add(KPri);
                //Pad.P.Add(NPri.dirNodo);
                r = 0;
                recorreDatosPad(Pad, index);
                
                r = 0;

                using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                {
                    bw.Seek(((int)Pad.dirNodo + 17) + ((index ) * 12), SeekOrigin.Begin);
                    r = 0;
                    bw.Write(KPri);
                    bw.Write(NPri.dirNodo);
                    r = 0;
                }
            }
            else
            {//dividi Padre
                r = 0;
                Nodo TP = new Nodo();
                copiaValAT(Pad, TP);
                r = 0;

                int index = Pad.P.FindIndex(x => x == N.dirNodo);
                TP.K.Insert(index, KPri);
                TP.P.Insert(index + 1, NPri.dirNodo);

                r = 0;

                Pad.K.Clear();
                Pad.P.Clear();

                
                r = 0;

                Nodo PadPrim = new Nodo();
                creaNodo(PadPrim, 'I');
                r = 0;

                repartirValoresT(TP, Pad, PadPrim,'P');
                r = 0;
                actulizarDivisionArchivo(Pad, PadPrim);
                r = 0;
                int KPadPri = TP.K.ElementAt(((n + 1) / 2)-1);

                r = 0;
                
                insert_in_parent(Pad, KPadPri, PadPrim);

                r = 0;

                


            }

            r = 0;

        }

        //Metodo que busca el nodo padre dado un nodo espesificado
        public Nodo buscaPadre(Nodo n)
        {
            r = 0;
            int index = lisNodPad.FindIndex(x => x.dirNodo == n.dirNodo);
            Nodo padre = lisNodPad.ElementAt(index-1);

            r = 0;
            return padre;
        }


        //Este metodo ayuda a repartir los valores entre el nodo respaldo que es T y los demas nodos que son L  y LPri
        public void repartirValoresT(Nodo T, Nodo L, Nodo LPri,char tipo)
        {
            r = 0;
            if (tipo == 'H')
            {
                for (int i = 0; i < (T.K.Count() - 1) - n / 2; i++)
                {
                    L.P.Add(T.P.ElementAt(i));
                    L.K.Add(T.K.ElementAt(i));
                }

                r = 0;
                for (int i = n / 2; i <= T.K.Count() - 1; i++)
                {
                    LPri.P.Add(T.P.ElementAt(i));
                    LPri.K.Add(T.K.ElementAt(i));
                }
                r = 0;
            }
            else
            {
                r = 0;
                for (int i = 0; i < (T.P.Count()) - (n+1) / 2; i++)
                {
                    L.P.Add(T.P.ElementAt(i));
                    L.K.Add(T.K.ElementAt(i));
                    
                }
                L.K.RemoveAt(L.K.Count - 1);

                r = 0;
                for (int i = (n+1) / 2; i <= T.P.Count()-1; i++)
                {
                    LPri.P.Add(T.P.ElementAt(i));
                    if(i != T.P.Count -1 )
                    LPri.K.Add(T.K.ElementAt(i));
                }
                r = 0;
            }

         }


        //Este metodo crea de forma general un nodo dado los paramatros recibidos
        public void creaNodo(Nodo LPri, char tipo)
        {
            if (tipo == 'H')
            {
                creaEspNodo('H');
                LPri.tipo = 'H';
            }
            else
            {
                creaEspNodo('I');
                LPri.tipo = 'I';
            }

            LPri.dirNodo = lisNodo.ElementAt(lisNodo.Count - 1).dirNodo + 65;
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {
                bw.Seek((int)LPri.dirNodo, SeekOrigin.Begin);
                bw.Write(LPri.dirNodo);
                bw.Write(LPri.tipo.ToString().ToCharArray());
            }

            lisNodo.Add(LPri);


        }

        /*
         * Metodo que actualiza los puntero a siguientes nodos en los nodos actuales
         */
        public void setPunteroSigHoja(Nodo L, Nodo LPri)
        {
            r = 0;
            archArb.Close();
            archArb = File.Open(archArb.Name, FileMode.Open);
            BinaryReader br = new BinaryReader(archArb);

            r = 0;
            
            archArb.Seek(L.dirNodo+57, SeekOrigin.Begin);
            long LPn = br.ReadInt64();

            r = 0;
            archArb.Close();
            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
            {
                bw.Seek((int)LPri.dirNodo + 57, SeekOrigin.Begin);
                bw.Write(LPn);
                r = 0;

                bw.Seek((int)L.dirNodo + 57, SeekOrigin.Begin);
                bw.Write(LPri.dirNodo);

                
                L.P.RemoveAt(n-1);
                r = 0;
                L.P.Add(LPri.dirNodo);
                r = 0;

            }

            archArb.Close();
        }


        //Metodo que encuentra un nodo en el arbol dada la clave resibida
        public Nodo encuentraNodoHoja(int V)
        {
            Nodo a = new Nodo();
            int valI = -1;
            long Pm = -1;
            int i = 0;
            lisNodPad = new List<Nodo>();
            r = 0;
            Nodo C = lisNodo.Find(x => x.tipo == 'R' && x.K.Count!= 0);

            lisNodPad.Add(C);

            r = 0;
            if (C.dirNodo == 0 && C.tipo == 'R')
            {
                r = 0;
                return C;
                
            }

            r = 0;

            while (C.tipo != 'H') 
            {
                r = 0;
                i = 0;
                valI = -1;
                Pm = -1;

                for (; i < C.K.Count; i++)
                {
                    if (V <= C.K.ElementAt(i))
                    {
                        r = 0;
                        valI = C.K.ElementAt(i);
                        break;
                    }
                }

                r = 0;

                if (C.tipo != 'H' && valI == -1 )
                    i--;

                r = 0;


                if (valI == -1)
                {
                    r = 0;
                    Pm = C.P.ElementAt(C.P.Count - 1);
                    C = lisNodo.Find(x => x.dirNodo == Pm);
                    r = 0;
                }
                else
                {
                    if (V == C.K.ElementAt(i))
                    {
                        r = 0;
                        C = lisNodo.Find(x => x.dirNodo == C.P.ElementAt(i + 1));
                        r = 0;
                    }
                    else
                    {
                        r = 0;
                        C = lisNodo.Find(x => x.dirNodo == C.P.ElementAt(i));
                        r = 0;
                    }
                }
                
                lisNodPad.Add(C);
                r = 0;

            }
            r = 0;

            return C;
        }




        //Metodo que inserta clave y direccion en el nodo especificado en las posiciones especificadas
        public void inset_in_leaf(Nodo L, int K, long P)
        {
            r = 0;
            if (L.K.Count == 0)//Es el primer nodo
            {
                r = 0;
                L.P.Insert(0, P);
                L.K.Insert(0, K);
                r = 0;
                using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                {
                    bw.Seek((int)L.dirNodo, SeekOrigin.Begin);
                    bw.Write(L.dirNodo);
                    bw.Write(L.tipo.ToString().ToCharArray());
                    bw.Write(P);
                    bw.Write(K);
                }


                r = 0;
            }
            else
            if (K < L.K.ElementAt(0))
            {
                r = 0;
                L.P.Insert(0, P);
                L.K.Insert(0, K);
                r = 0;
            }
            else
            {
                r = 0;

                for (int i = L.K.Count() - 1; i >= 0; i--)
                {
                    r = 0;
                    if (L.K.ElementAt(i) <= K)
                    {
                        r = 0;
                        L.K.Insert(i+1, K);
                        L.P.Insert(i+1, P);
                        r = 0;
                        

                        if (!bandT)
                        {
                            r = 0;
                            recorreDatos(L, i + 1);
                            using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                            {
                                bw.Seek(((int)L.dirNodo + 9) + ((i + 1) * 12), SeekOrigin.Begin);
                                r = 0;
                                bw.Write(P);
                                bw.Write(K);
                                r = 0;
                            }
                        }
                        r = 0;
                        break;
                    }
                    


                }

                if(L.P.Count == n-1)
                {
                    r = 0;
                    archArb.Close();
                    archArb = File.Open(archArb.Name, FileMode.Open);
                    BinaryReader br = new BinaryReader(archArb);
                    archArb.Seek((int)L.dirNodo + 57, SeekOrigin.Begin);
                    L.P.Add(br.ReadInt64());
                    archArb.Close();
                    
                }
                r = 0;
            }
        }

        //Metodo que recorre valores de forma fisica en el nodo para que asi queden en la adecuada posicion
        public void recorreDatos(Nodo L, int indice)
        {

            r = 0;
            archArb = File.Open(archArb.Name, FileMode.Open);
            BinaryReader br2 = new BinaryReader(archArb);

            archArb.Seek(((int)L.dirNodo + 9) + (( indice) * 12), SeekOrigin.Begin);

            long longDir;
            int clave;


            longDir = br2.ReadInt64();
            clave = br2.ReadInt32();


            int auxPos = L.K.Count-1;

            r = 0;

            while (longDir != -1 && indice != auxPos )
            {
                r = 0;

                archArb.Close();
                archArb = File.Open(archArb.Name, FileMode.Open);
                br2 = new BinaryReader(archArb);
                archArb.Seek(((int)L.dirNodo + 9) + ((auxPos - 1) * 12), SeekOrigin.Begin);
                longDir = br2.ReadInt64();
                clave = br2.ReadInt32();

                r = 0;
                archArb.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                {
                    bw.Seek(((int)L.dirNodo + 9) + ((auxPos) * 12), SeekOrigin.Begin);
                    bw.Write(longDir);
                    bw.Write(clave);
                    r = 0;
                }

                r = 0;
                auxPos--;
                r = 0;
                archArb = File.Open(archArb.Name, FileMode.Open);
                BinaryReader br3 = new BinaryReader(archArb);

                archArb.Seek(((int)L.dirNodo + 9) + ((auxPos - 1) * 12), SeekOrigin.Begin);
                longDir = br3.ReadInt64();

                r = 0;
                
                


            }
            archArb.Close();
            r = 0;
        }

        
        //Metodo que igual recorre datos de forma fisica pero en un nodo padre
        public void recorreDatosPad(Nodo L, int indice)
        {

            r = 0;
            archArb = File.Open(archArb.Name, FileMode.Open);
            BinaryReader br2 = new BinaryReader(archArb);

            archArb.Seek(((int)L.dirNodo + 9) + ((indice) * 12), SeekOrigin.Begin);

            long longDir;
            int clave;
            bool hayClave = true;


            longDir = br2.ReadInt64();



            int auxPos = L.P.Count - 1;

            r = 0;

            while (longDir != -1 && indice != auxPos)
            {
                r = 0;

                archArb.Close();
                archArb = File.Open(archArb.Name, FileMode.Open);
                br2 = new BinaryReader(archArb);
                archArb.Seek(((int)L.dirNodo + 9) + ((auxPos-1)*12), SeekOrigin.Begin);
                longDir = br2.ReadInt64();

                clave = br2.ReadInt32();
                r = 0;

                if (clave == -1)
                {
                    hayClave = false;
                }
                r = 0;

                archArb.Close();
                using (BinaryWriter bw = new BinaryWriter(File.Open(archArb.Name, FileMode.Open)))
                {
                    bw.Seek(((int)L.dirNodo + 9) + ((auxPos) * 12), SeekOrigin.Begin);
                    bw.Write(longDir);

                    if (hayClave)
                    {
                        bw.Write(clave);
                    }
                    r = 0;
                }

                hayClave = true;
                r = 0;
                auxPos--;
                r = 0;
                archArb = File.Open(archArb.Name, FileMode.Open);
                BinaryReader br3 = new BinaryReader(archArb);

                archArb.Seek(((int)L.dirNodo + 9) + ((auxPos - 1) * 12), SeekOrigin.Begin);
                longDir = br3.ReadInt64();

                r = 0;

            }
            archArb.Close();
            r = 0;
        }

      



    }
}
