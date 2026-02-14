using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public class FrageService : IFrageService
    {
        public async Task<List<Frage>> GetAllQuestions()
        {

            List<Frage> fragen = new List<Frage>()
            {
                new Frage()
                {
                    Id = 1,
                    Name = "Welches UML-Diagramm wird hauptsächlich verwendet, um die statische Struktur eines Systems darzustellen?",
                    
                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 1,
                            Name = "Sequenzdiagramm"
                        },
                        new Antwort()
                        {
                            Id = 2,
                            Name = "Aktivitätsdiagramm"
                        },
                        new Antwort()
                        {
                            Id = 3,
                            Name = "Klassendiagramm"
                        },
                        new Antwort()
                        {
                            Id = 4,
                            Name = "Zustandsdiagramm"
                        }
                    }
                },
                new Frage()
                {
                    Id = 2,
                    Name = "Welche Beziehung wird im UML-Klassendiagramm durch eine durchgezogene Linie mit einer Raute dargestellt?",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 5,
                            Name = "Komposition"
                        },
                        new Antwort()
                        {
                            Id = 6,
                            Name = "Aggregation"
                        },
                        new Antwort()
                        {
                            Id = 7,
                            Name = "Assoziation"
                        },
                        new Antwort()
                        {
                            Id = 8,
                            Name = "Generalisierung"
                        }
                    }
                },
                new Frage()
                {
                    Id = 3,
                    Name = "Welches UML-Diagramm zeigt die Interaktion zwischen Objekten in zeitlicher Reihenfolge?",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 5,
                            Name = "Komposition"
                        },
                        new Antwort()
                        {
                            Id = 6,
                            Name = "Aggregation"
                        },
                        new Antwort()
                        {
                            Id = 7,
                            Name = "Assoziation"
                        },
                        new Antwort()
                        {
                            Id = 8,
                            Name = "Generalisierung"
                        }
                    }
                },
                new Frage()
                {
                    Id = 4,
                    Name = "Welche Art von Beziehung beschreibt eine Vererbung zwischen zwei Klassen?",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 5,
                            Name = "Komposition"
                        },
                        new Antwort()
                        {
                            Id = 6,
                            Name = "Aggregation"
                        },
                        new Antwort()
                        {
                            Id = 7,
                            Name = "Assoziation"
                        },
                        new Antwort()
                        {
                            Id = 8,
                            Name = "Generalisierung"
                        }
                    }
                },
                new Frage()
                {
                    Id = 5,
                    Name = "Welches Diagramm eignet sich am besten, um den Ablauf eines Prozesses mit Verzweigungen und Schleifen darzustellen?",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 5,
                            Name = "Komposition"
                        },
                        new Antwort()
                        {
                            Id = 6,
                            Name = "Aggregation"
                        },
                        new Antwort()
                        {
                            Id = 7,
                            Name = "Assoziation"
                        },
                        new Antwort()
                        {
                            Id = 8,
                            Name = "Generalisierung"
                        }
                    }
                },
                new Frage()
                {
                    Id = 6,
                    Name = "Wofür wird ein Aktivitätsdiagramm hauptsächlich verwendet?",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 5,
                            Name = "Komposition"
                        },
                        new Antwort()
                        {
                            Id = 6,
                            Name = "Aggregation"
                        },
                        new Antwort()
                        {
                            Id = 7,
                            Name = "Assoziation"
                        },
                        new Antwort()
                        {
                            Id = 8,
                            Name = "Generalisierung"
                        }
                    }
                }
            };





            return fragen.ToList();
        }
    }
}
