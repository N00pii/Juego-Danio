class Juego{
    public static void Main(){

        Personaje sacerdote = new Sacerdote("Samson", 30, 5);
        Personaje barbaro = new Barbaro("Dave", 30, 7, 10);

        Equipo tunica = new Armadura(5);
        Equipo hacha = new Arma(6);

        sacerdote.Equipar (tunica);
        barbaro.Equipar (hacha);

        Personaje? ganador = Batalla(sacerdote,barbaro);

        if(ganador == null) {
            Console.WriteLine("La batallita termino en EMPATE");
            return;
        }
        else{
            Console.WriteLine($"El ganador de esta batallita es {ganador.getNombre()}");
            Console.WriteLine("");
        }
        
        string arqNombre = "";
        int arqVida = 1;
        int arqAtaque = 1;
        
        do{
            try{
                Console.Write("Ingrese el nombre del Arquero: ");
                arqNombre = Console.ReadLine();

                if (arqNombre != "") {
                    continue;
                }
            }
            catch{
                Console.Write("Escriba bien. ");
            }
        }
        while (arqNombre==null);

        do {
            try{
                Console.Write("Ingrese la vida del Arquero: ");
                arqVida=int.Parse(Console.ReadLine());

                if (arqVida<1){
                    continue;
                }
            }
            catch{
                Console.Write("Escriba bien. ");
            }
        }
        
        while (arqVida<1);
        
        do {
            try{
                Console.Write("Ingrese el ataque del Arquero: ");
                arqAtaque=int.Parse(Console.ReadLine());

                if (arqAtaque<1){
                    continue;
                }
            }
            catch{
                Console.Write("Escriba bien. ");
            }
        } while (arqAtaque<1);

        Personaje arquero = new Arquero(arqNombre, arqVida, arqAtaque);

        Personaje? ganador2 = Batalla(arquero, ganador);

    }

//--------------------------------BATALLA--------------------------

    public static Personaje Batalla(Personaje p1, Personaje p2){
        
        int turno = 1;
        Console.WriteLine("");
        while (p1.getVida()>0 && p2.getVida()>0) {

            Console.WriteLine($"-----------------RONDA {turno}------------------");

            Console.Write($"{p1.getNombre()} ataca a {p2.getNombre()}. ");

            if (p1.getVida()==0) {
                Console.WriteLine("Ha muerto.");
            }
            else{
                Console.WriteLine("");
            }
            p1.atacar(p2);

            Console.Write($"{p2.getNombre()} ataca a {p1.getNombre()}. ");

            if (p2.getVida()==0) {
                Console.WriteLine("Ha muerto.");
            }
            else {
                Console.WriteLine("");
            }

            p2.atacar(p1);

            Console.WriteLine("");
            turno++;
        }

        if (p1.getVida()==0 && p2.getVida()>0) {
            Console.WriteLine($"{p1.getNombre()} ha muerto");
            return p2;
        }

        else if (p1.getVida()>0 && p2.getVida()==0) {
            Console.WriteLine($"{p2.getNombre()} ha muerto");
            return p1;         
        }

        else {
            return null;
        }
    }
}

//----------------------------PERSONAJE----------------------------

class Personaje {
    private string nombre {get; set;}
    private int vida {get; set;}
    private int ataque {get; set;}
    private Equipo? equipo {get; set;}

    public Personaje (string nombre, int vida, int ataque) {
        this.nombre = nombre;
        this.vida = vida;
        this.ataque = ataque;
        this.equipo = null;
    }

    public string getNombre() {
        return nombre;
    }

    public int getVida() {
        return vida;
    }

    public int getAtaque() {
        int danioTotal= ataque;
        if (equipo != null) {
            danioTotal = danioTotal+equipo.getModificadorAtaque();
        }
        return danioTotal;
    }

    public int getArmadura() {

        int armadura = 0;
        if (equipo != null) {
            armadura = equipo.getModificadorArmadura();
        }
        return armadura;
    }

    public virtual void atacar(Personaje objetivo) {
        int danioTotal = ataque;
        objetivo.recibirDanio(danioTotal);
    }

    public virtual void recibirDanio(int danio) {

        int danioRecibido = danio;

        danioRecibido = danioRecibido - getArmadura();

        if (danioRecibido < 1) {
            danioRecibido = 1;
        } 

        vida = vida - danioRecibido;

        Console.WriteLine($"{getNombre()} recibe {danioRecibido} puntos de daño");

         if (vida < 0) {
            vida = 0;
         }
    }

    public void Equipar(Equipo equipo) {
        this.equipo = equipo;
    }
}

//-------------------------------BARBARO----------------------------

class Barbaro : Personaje {
    private int furia {get; set;}

    public Barbaro (string nombre, int vida, int ataque, int furia) : base (nombre,vida, ataque) {
        this.furia = furia;
    }

    public override void atacar (Personaje objetivo) {
        
        int nuevoDanio = getAtaque();
        if (furia >= 3) {
            nuevoDanio = (int)Math.Round(nuevoDanio * 1.15m); 
            furia = furia - 3;
            Console.WriteLine($"{getNombre()} ataca furioso");
        }

        else {
            nuevoDanio = (int)Math.Round(nuevoDanio / 2m);
            Console.WriteLine($"{getNombre()} está cansado");
        }

        objetivo.recibirDanio (nuevoDanio);
    }
}

//----------------------------SACERDOTE-------------------------------

class Sacerdote : Personaje {
    public Sacerdote (string nombre, int vida, int ataque) : base (nombre,vida, ataque) {}

    public override void recibirDanio (int danio) {
        Random reductorDanio = new Random ();
        int probabilidad = reductorDanio.Next (0 , 4);
        int nuevoDanio = getAtaque();
        if (probabilidad == 1) {
            nuevoDanio = (int)Math.Round(getAtaque() / 2m);
            Console.WriteLine($"Las plegarias de {getNombre()} han sido escuchadas");
        }
        base.recibirDanio(nuevoDanio);
    }
}

//-----------------------ARQUERO---------------------------------------

class Arquero : Personaje {
    public Arquero (string nombre, int vida, int ataque) : base (nombre, vida, ataque) {}

    public override void atacar(Personaje objetivo) {
        Random multiplicadorHeadShot = new Random ();
        int probabilidad = multiplicadorHeadShot.Next (0 , 2);
        int nuevoDanio = getAtaque();
        if (probabilidad == 1) {
            nuevoDanio = (int)Math.Round(getAtaque() * 1.5m);
            Console.WriteLine($"{getNombre()} acaba de dar un tiro preciso");
        }
        objetivo.recibirDanio(nuevoDanio);
    }
}

//---------------------------EQUIPO------------------------------------

class Equipo {
    private int modificadorAtaque {get; set;}
    private int modificadorArmadura {get; set;}
    
    public Equipo (int modificadorAtaque, int modificadorArmadura) {
        this.modificadorAtaque = modificadorAtaque;
        this.modificadorArmadura = modificadorArmadura;
    }

    public int getModificadorAtaque(){
        return modificadorAtaque;
    }

    public int getModificadorArmadura(){
        return modificadorArmadura;
    }
}

//-----------------------------ARMA--------------------------------

class Arma : Equipo {
    public Arma (int modificadorAtaque) : base (modificadorAtaque,0) {}
}

//---------------------------ARMADURA---------------------------------

class Armadura : Equipo {
    public Armadura (int modificadorArmadura) : base (modificadorArmadura,0) {}
}