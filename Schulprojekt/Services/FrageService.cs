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
                            Id = 9,
                            Name = "Komponentendiagramm"
                        },
                        new Antwort()
                        {
                            Id = 10,
                            Name = "Use-Case-Diagramm"
                        },
                        new Antwort()
                        {
                            Id = 11,
                            Name = "Sequenzdiagramm"
                        },
                        new Antwort()
                        {
                            Id = 12,
                            Name = "Paketdiagramm"
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
                            Id = 13,
                            Name = "Abhängigkeit"
                        },
                        new Antwort()
                        {
                            Id = 14,
                            Name = "Assoziation"
                        },
                        new Antwort()
                        {
                            Id = 15,
                            Name = "Generalisierung"
                        },
                        new Antwort()
                        {
                            Id = 16,
                            Name = "Realisation"
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
                            Id = 17,
                            Name = "Aktivitätsdiagramm"
                        },
                        new Antwort()
                        {
                            Id = 18,
                            Name = "Klassendiagramm"
                        },
                        new Antwort()
                        {
                            Id = 19,
                            Name = "Zustandsdiagramm"
                        },
                        new Antwort()
                        {
                            Id = 20,
                            Name = "Objektdiagramm"
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
                            Id = 21,
                            Name = "Darstellung der Klassenhierarchie"
                        },
                        new Antwort()
                        {
                            Id = 22,
                            Name = "Modellierung von Abläufen und Workflows"
                        },
                        new Antwort()
                        {
                            Id = 23,
                            Name = "Beschreibung der Systemarchitektur"
                        },
                        new Antwort()
                        {
                            Id = 24,
                            Name = "Darstellung von Objektinteraktionen über Zeit"
                        }
                    }
                },
                new Frage()
                {
                    Id = 7,
                    Name = "Welche Darstellung gehört typischerweise zu einem Zustandsdiagramm?",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 25,
                            Name = "Lebenslinien"
                        },
                        new Antwort()
                        {
                            Id = 26,
                            Name = "Zustände und Übergänge"
                        },
                        new Antwort()
                        {
                            Id = 27,
                            Name = "Komponenten und Schnittstellen"
                        },
                        new Antwort()
                        {
                            Id = 28,
                            Name = "Pakete und Abhängigkeiten"
                        }
                    }
                },
                new Frage()
                {
                    Id = 8,
                    Name = "Welche Beziehung wird im Klassendiagramm durch eine gestrichelte Linie mit einem offenen Pfeil dargestellt?",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 29,
                            Name = "Assoziation"
                        },
                        new Antwort()
                        {
                            Id = 30,
                            Name = "Realisierung"
                        },
                        new Antwort()
                        {
                            Id = 31,
                            Name = "Komposition"
                        },
                        new Antwort()
                        {
                            Id = 32,
                            Name = "Aggregation"
                        }
                    }
                },
                new Frage()
                {
                    Id = 9,
                    Name = "Wofür wird ein Use-Case-Diagramm verwendet?",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 33,
                            Name = "Darstellung der Systemlogik"
                        },
                        new Antwort()
                        {
                            Id = 34,
                            Name = "Beschreibung der Interaktion zwischen Akteuren und System"
                        },
                        new Antwort()
                        {
                            Id = 35,
                            Name = "Modellierung der Datenbankstruktur"
                        },
                        new Antwort()
                        {
                            Id = 36,
                            Name = "Darstellung der zeitlichen Abfolge von Nachrichten"
                        }
                    }
                },
                new Frage()
                {
                    Id = 10,
                    Name = "Was zeigt ein Objektdiagramm?",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 37,
                            Name = "Konkrete Instanzen von Klassen zu einem bestimmten Zeitpunkt"
                        },
                        new Antwort()
                        {
                            Id = 38,
                            Name = "Die gesamte Klassenhierarchie"
                        },
                        new Antwort()
                        {
                            Id = 39,
                            Name = "Die Kommunikation zwischen Komponenten"
                        },
                        new Antwort()
                        {
                            Id = 40,
                            Name = "Die möglichen Zustände eines Objekts"
                        }
                    }
                }
            };
            




            return fragen.ToList();
        }
        // Refs #10: Lückentext-Fragen
        public async Task<List<Frage>> GetLueckentextQuestions()
        {
            await Task.CompletedTask;

            return new List<Frage>
            {
                new Frage
                {
                    Id = 1001,
                    Name = "TCP ist ____ (connection-oriented).",
                    Antworten = new List<Antwort>() // leer => Freitext
                },
                new Frage
                {
                    Id = 1002,
                    Name = "Der Standard-Port für HTTPS ist ____.",
                    Antworten = new List<Antwort>()
                },
                new Frage
                {
                    Id = 1003,
                    Name = "DNS verwendet standardmäßig Port ____ (UDP).",
                    Antworten = new List<Antwort>()
                },
                new Frage
                {
                    Id = 1004,
                    Name = "In UML zeigt ein offenes Dreieck meist ____ (Beziehung).",
                    Antworten = new List<Antwort>()
                }
            };
        }

    }
}
