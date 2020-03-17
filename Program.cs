using ProjetLinq.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Auteurs
{
    class Program
    {
        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }


        static void Main(string[] args)
        {
            InitialiserDatas();

            Console.WriteLine("Liste des prénoms des auteurs dont le nom commence par 'G'");
            var listePrenomCommencantParG = ListeAuteurs.Where(a => a.Nom.StartsWith("G")).Select(a => a.Prenom);
            foreach (var prenom in listePrenomCommencantParG)
            {
                Console.WriteLine(prenom);
            }

            Console.WriteLine();
            Console.WriteLine("L'auteur ayant écrit le plus de livres est : ");
            try
            {
                //Key => obtient la clé du GroupBy : ici auteur
                var auteurEcritLePlusDeLivre = ListeLivres.GroupBy(l => l.Auteur).OrderByDescending(a => a.Count()).First().Key;
                Console.WriteLine(auteurEcritLePlusDeLivre.Nom + " " + auteurEcritLePlusDeLivre.Prenom);
            } catch(Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Le nombre moyen de pages par livre par auteur est : ");
            var livresParAuteur = ListeLivres.GroupBy(l => l.Auteur);
            foreach (var livre in livresParAuteur)
            {
                //Key => obtient la clé du GroupBy : ici auteur
                Auteur auteur = livre.Key;
                double nbPages = livre.Average(l=> l.NbPages);
                Console.WriteLine($"{auteur.Nom} {auteur.Prenom} a écrit en moyenne {nbPages} pages");
            }

            Console.WriteLine();
            Console.WriteLine("Le titre du livre avec le plus de pages est : ");
            try { 
                var titreLePlusDePages = ListeLivres.OrderByDescending(l=>l.NbPages).Select(l=>l.Titre).First();
                Console.WriteLine(titreLePlusDePages);
            } catch(Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Combien ont gagné les auteurs en moyenne ?");
            var moyenneFacture = ListeAuteurs.Average(a => a.Factures.Sum(f => f.Montant));
            Console.WriteLine("Ils ont gagné ensemble en moyenne " + moyenneFacture);

            Console.WriteLine();
            Console.WriteLine("Les auteurs et la liste de leurs livres : ");
            var auteurByLivre = ListeLivres.GroupBy(l => l.Auteur);
            foreach(var livres in auteurByLivre)
            {
                Auteur auteur = livres.Key;
                Console.WriteLine($"{auteur.Nom} {auteur.Prenom} a écrit : ");
                foreach (var livre in livres)
                {
                    Console.WriteLine($" • {livre.Titre}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Titres des livres triés par ordre alphabetique :");
            var livresOrderByTitre = ListeLivres.OrderBy(l => l.Titre).Select(l => l.Titre);
            foreach (var titre in livresOrderByTitre)
            {
                Console.WriteLine(titre);
            }

            Console.WriteLine();
            Console.WriteLine("Livres dont le nombre de pages est supérieur à la moyenne :");
            var moyennePages = ListeLivres.Average(l => l.NbPages);
            Console.WriteLine("La moyenne de pages est " + moyennePages);
            var livresAvecPlusDePageQueLaMoyenne = ListeLivres.Where(l => l.NbPages > moyennePages);
            foreach (var livre in livresAvecPlusDePageQueLaMoyenne)
            {
                Console.WriteLine($"Le livre {livre.Titre} de {livre.Auteur.Nom} {livre.Auteur.Prenom} avec {livre.NbPages} pages.");
            }

            Console.WriteLine();
            Console.WriteLine("L'auteur ayant écrit le moins de livre est :");
            var auteurMoinsDeLivres = ListeLivres.GroupBy(l => l.Auteur).OrderBy(a => a.Count()).FirstOrDefault().Key;
            Console.WriteLine($"{auteurMoinsDeLivres.Nom} {auteurMoinsDeLivres.Prenom}");


            Console.ReadKey();
        }
}
}
