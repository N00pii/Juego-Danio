class Juego{
    public static void Main(){

        Personaje sacerdote = new Sacerdote("Samson", 22, 4);
        Personaje barbaro = new Barbaro("Dave", 20, 5, 2);
        Personaje arquero = new Arquero("Huntsmann", 15, 4); // ROBO DE VIDA PENDIENTE

        Equipo tunica = new Armadura(5);
        Equipo hacha = new Arma(8);

        sacerdote.Equipar (tunica);
        barbaro.Equipar (hacha);

        Personaje ganador=Batalla(sacerdote,barbaro);
        //sssssssssssssssssssssss
    }

    public static Personaje Batalla(Personaje p1, Personaje p2){
        // Simular la batalla
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
        return ataque;
    }

    public int getArmadura() {
        return ataque;
    }

    public virtual void atacar(Personaje objetivo) {}

    public virtual void recibirDanio(int danio) {}

    public void equipar(Equipo equipo) {}
}

//-------------------------------BARBARO----------------------------

class Barbaro : Personaje {
    private int furia {get; set;}

    public Barbaro (string nombre, int vida, int ataque, int furia) : base (nombre,vida, ataque) {
        this.furia = furia;
    }

    public override void atacar (Personaje objetivo) {}
}

//----------------------------SACERDOTE-------------------------------

class Sacerdote : Personaje {
    public Sacerdote (string nombre, int vida, int ataque) : base (nombre,vida, ataque) {}

    public override void recibirDanio (int danio) {}
}

//-----------------------ARQUERO---------------------------------------

class Arquero : Personaje {
    public Arquero (string nombre, int vida, int ataque) : base (nombre, vida, ataque) {}

    public override void atacar(Personaje objetivo) {}
}

//---------------------------EQUIPO------------------------------------

class Equipo {
    private int modificadorAtaque {get; set;}
    private int modificadorArmadura {get; set;}
    
    public Equipo (int modificadorAtaque, int modificadorArmadura) {
        this.modificadorAtaque = modificadorAtaque;
        this.modificadorArmadura = modificadorArmadura;
    }
}

//-----------------------------ARMA--------------------------------

class Arma : Equipo {
    private Arma (int modificadorAtaque) : base (modificadorAtaque,0) {}
}

//---------------------------ARMADURA---------------------------------

class Armadura : Equipo {
    private Armadura (int modificadorArmadura) : base (modificadorArmadura,0) {}
}