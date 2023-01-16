class Essai
{
    public Essai(int nombre, ResultatEssai resultat)
    {
        this._NombreTente = nombre;
        this.Resultat = resultat;
    }
    #region NombreTente
    private int _NombreTente;
    public int NombreTente
    {
        get { return this._NombreTente; }
    }
    #endregion
    public ResultatEssai Resultat;
}

