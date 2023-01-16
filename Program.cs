// See https://aka.ms/new-console-template for more information

// Impossible de créer une partie sans nom valable
// Partie p = new Partie("Ben");
// var j = p.NomDuJoueur;

// Impossible de changer le nom du joueur (fourni lots de la construction)
// p.NomDuJoueur="Dominique";

// Impossible de changer l'état de la partie
// p.Etat=EtatPartie.Gagnee;

// Impossible d'avoir un NombreEssaisMax<=0
// p.NombreEssaisMax=-3;
// Impossible de commencer une partie avec NombreMin>NombreMax
// p.NombreMin = 100;
// p.NombreMax = 10;
// p.Commencer();

// Impossible de commencer une partie commencee
// p.Commencer();
// Impossible de modifier les paramètre d'une partie commencee
// p.NombreEssaisMax=4;
// p.NombreMin=100;
// p.NombreMax=10;
// Impossible de lire la réponse quand la partie n'est pas terminee
// var r=p.NombreAleatoire

// try {
//     Essai r = p.Tenter(12);
// }
// catch (InvalidOperationException) {
//     Console.WriteLine("C'est pas le moment");
// }
// catch (ArgumentOutOfRangeException) {
//     Console.WriteLine("Le nombre n'est pas dans les limites du jeux");
// }
// catch(Exception) {
//     Console.WriteLine("Un autre probléme est survenu");
// }

static int GetIntFromUser(string message, int minimum=int.MinValue, int maximum=int.MaxValue)
{
    while (true)
    {
        Console.Write(message);
        string UserInput = Console.ReadLine();
        try
        {
            int NombreEntre; 
            try
            {      
                   NombreEntre = int.Parse(UserInput);
            }
            catch (System.Exception)
            {
                
                throw new Exception("Entrez un entier");
            }
          
            if(NombreEntre<minimum || NombreEntre>maximum ){
                // Constitution du message erreur
                var MessageErreur=string.Format("Votre entier doit être entre {0} et {1}", minimum, maximum);
                throw new Exception(MessageErreur);
                
            }
            return NombreEntre;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(ex.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
 
}

while(true){
    Console.WriteLine("Nouvelle partie !");
    Console.WriteLine("Entrez votre nom : ");
    string NomJoueur = Console.ReadLine();

    Partie PartieEnCours = new Partie(NomJoueur);

    PartieEnCours.NombreMin=GetIntFromUser("Entrez le nombre minimum : ");
    PartieEnCours.NombreMax=GetIntFromUser("Entrez le nombre maximum : ");
    PartieEnCours.NombreEssaisMax=GetIntFromUser("Entrez le nombre d'essais maximum : ");

    PartieEnCours.Commencer();

    while(!PartieEnCours.EstTerminee) {

        string message=string.Format("Entrez le nombre que vous voulez tenter {0}/{1}:",PartieEnCours.NombreEssaisRealises+1, PartieEnCours.NombreEssaisMax);
        int nombreTente=GetIntFromUser("Entrez le nombre que vous voulez tenter : ");
        var essai = PartieEnCours.Tenter(nombreTente);
        
        // En fonction de essai.Resultat => switch
        switch (essai.Resultat)
        {
            case ResultatEssai.Egal:
                Console.WriteLine("Votre nombre est le bon!");
                break;
            case ResultatEssai.TropGrand:
                Console.WriteLine("Votre nombre est trop grand");
                break;
            case ResultatEssai.TropPetit:
                Console.WriteLine("Votre nombre est trop petit");
                break;    
        }
    }

    switch(PartieEnCours.Etat) {
        case EtatPartie.Gagnee:
            Console.WriteLine("Gagné");
            break;
        case EtatPartie.Perdue:
            Console.WriteLine("Perdu, le nombre était : {0}", PartieEnCours.NombreAleatoire);
            break;       
    }
}