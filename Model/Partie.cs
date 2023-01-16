class Partie
{
    // Constructeur avec paramètre nom
    public Partie(string nom)
    {
        // Je stocke le nom passé en paramètre dans la case (champ = field)
        this.NomDuJoueur = nom;
    }

    #region NomDuJoueur
    private string? _NomDuJoueur = null;
    public string? NomDuJoueur
    {
        get { return this._NomDuJoueur; }
        private set
        {
            if (this.Etat != EtatPartie.NonCommencee)
            {
                throw new Exception("Le nom ne peut être changé une fois la partie commencée");
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("Le nom du joueur est vide");
            }

            this._NomDuJoueur = value;
        }
    }
    #endregion

    #region NombreMin

    private int _NombreMin = 0;
    public int NombreMin
    {
        get { return this._NombreMin; }
        set
        {
            if (this.Etat != EtatPartie.NonCommencee)
            {
                throw new Exception("Tu ne peux plus modifier le nombre min");
            }

            this._NombreMin = value;
        }
    }
    #endregion

    #region NombreMax
    private int _NombreMax = 100;
    public int NombreMax
    {
        get { return this._NombreMax; }
        set
        {
            if (this.Etat != EtatPartie.NonCommencee)
            {
                throw new Exception("Tu ne peux plus modifier le nombre max");
            }

            this._NombreMax = value;
        }
    }
    #endregion

    #region NombreEssaisMax
    private int _NombreEssaisMax = 6;
    public int NombreEssaisMax
    {
        get { return this._NombreEssaisMax; }
        set
        {
            if (this.Etat != EtatPartie.NonCommencee)
            {
                throw new Exception("La partie est en cours");
            }
            if (value < 1)
            {
                throw new Exception("1 essai minimum");
            }

            this._NombreEssaisMax = value;
        }
    }
    #endregion

    #region NombreAleatoire
    private int? _NombreAleatoire;
    public int? NombreAleatoire
    {
        get
        {
            if (this.Etat != EtatPartie.Gagnee && this.Etat != EtatPartie.Perdue)
            {
                throw new Exception("Lecture impossible tant que la partie n'est pas terminer");
            }
            // _NombreAleatoire ne peut plus être null ici
            return this._NombreAleatoire.Value;
        }
    }
    #endregion

    #region Etat
    private EtatPartie _Etat = EtatPartie.NonCommencee;
    public EtatPartie Etat
    {
        get { return this._Etat; }
        // Set de état non codé
        // Donc lapropriété est en lecture seule, elle ne peut pas être modifier
    }
    #endregion

    public double Difficulte
    {
        get
        {
            return Math.Abs(this.NombreMax - this.NombreMin) / Math.Pow(2, NombreEssaisMax);
        }
    }

    public bool EstTerminee
    {
        get
        {
            return this.Etat == EtatPartie.Gagnee || this.Etat == EtatPartie.Perdue;
        }
    }

    public int NombreEssaisRestant {
        get {
        return this.NombreEssaisMax-this.NombreEssaisRealises;
        }
    }

    public int NombreEssaisRealises {
        get {
        return this.Essais.Count;
        }
    }

    public List<Essai> Essais = new List<Essai>();

    public void Commencer()

    {
        if (this.Etat != EtatPartie.NonCommencee)
        {
            throw new Exception("La partie est déja commencée");
        }
        if (this.NombreMin > this.NombreMax)
        {
            int temp = this.NombreMin;
            this.NombreMin = this.NombreMax;
            this.NombreMax = temp;
        }
        this._Etat = EtatPartie.EnCours;
        this._NombreAleatoire = new Random().Next(this.NombreMin, this.NombreMax + 1);
    }

    public Essai Tenter(int nombreTente)
    {
        // Partie en cours pour tenter 
        if (this.Etat != EtatPartie.EnCours)
        {
            throw new InvalidOperationException("on peut tenter uniquement si la partie est en cours");
        }

        // Mode gentil : l'entrée est validée avant de créer l'essai
        if (nombreTente > this.NombreMax || nombreTente < this.NombreMin)
        {
            throw new ArgumentOutOfRangeException("Nombre tenté pas dans les clous");
        }
        // Je déclare l'essai sans lui donner de valeur
        Essai e;
        if (nombreTente == this._NombreAleatoire)
        {   
            // Car essai réussi
            e = new Essai(nombreTente, ResultatEssai.Egal);
            this._Etat=EtatPartie.Gagnee;
        } else
        // On sait que nombreTenté n'est pas = 
        if (nombreTente < this._NombreAleatoire)
        {
            // Car essai trop petit
            e = new Essai(nombreTente, ResultatEssai.TropPetit);
        } else
        // On sait que nombreTenté n'est pas =, ni plus petit
        e = new Essai(nombreTente, ResultatEssai.TropGrand);

        this.Essais.Add(e);
        
        if (this.Etat != EtatPartie.Gagnee && this.NombreEssaisRestant==0)
        {
            this._Etat = EtatPartie.Perdue;
        }
        return e;
    }
}