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

        public async Task<List<Frage>> GetAllTrueFalseQuestions()
        {

            List<Frage> fragen = new List<Frage>()
            {
                new Frage()
                {
                    Id = 11,
                    Name = "Eine abstrakte Klasse kann keine Instanzen besitzen.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 41,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 42,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 12,
                    Name = "Eine Aggregation ist stärker als eine Komposition.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 43,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 44,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 13,
                    Name = "Eine Assoziation kann eine Multiplizität besitzen.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 45,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 46,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 14,
                    Name = "Interfaces dürfen Attribute besitzen.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 47,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 48,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 15,
                    Name = "Eine Generalisierung beschreibt eine \"ist-ein\"-Beziehung.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 49,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 50,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 16,
                    Name = "Eine Assoziationsklasse kann sowohl Attribute als auch Operationen besitzen.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 51,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 52,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 17,
                    Name = "Eine Komposition erlaubt, dass das Teilobjekt auch ohne das Ganze weiterexistiert.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 53,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 54,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 18,
                    Name = "Ein Interface kann von mehreren Interfaces erben.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 55,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 56,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 19,
                    Name = "Eine Abhängigkeit ist stärker als eine Assoziation.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 57,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 58,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 20,
                    Name = "Eine Klasse kann gleichzeitig abstrakt und final sein.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 59,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 60,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 21,
                    Name = "Eine Assoziationsklasse kann selbst wiederum in einer weiteren Assoziation verwendet werden.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 61,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 62,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 22,
                    Name = "Ein Interface darf in UML eigene Attribute besitzen, solange sie als \"public\" deklariert sind.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 63,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 64,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 23,
                    Name = "Eine Klasse kann gleichzeitig eine Realisierung und eine Generalisierung zu demselben Ziel besitzen.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 65,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 66,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 24,
                    Name = "Eine qualifizierte Assoziation schränkt die Identifikation der Zielobjekte über einen Schlüssel ein.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 67,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 68,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 25,
                    Name = "Eine Abhängigkeitsbeziehung kann zyklisch sein, ohne dass dies ein Modellierungsproblem darstellt.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 69,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 70,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 26,
                    Name = "Ein Aktivitätsdiagramm kann sowohl Kontroll- als auch Objektflüsse enthalten.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 71,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 72,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 27,
                    Name = "Ein Entscheidungsknoten darf nur zwei ausgehende Kanten besitzen.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 73,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 74,
                            Name = "Falsch"
                        }
                    }
                },
                new Frage()
                {
                    Id = 28,
                    Name = "Ein Startknoten hat genau einen ausgehenden Kontrollfluss.",

                    Antworten = new List<Antwort>()
                    {
                        new Antwort()
                        {
                            Id = 75,
                            Name = "Wahr"
                        },
                        new Antwort()
                        {
                            Id = 76,
                            Name = "Falsch"
                        }
                    }
                }
            };

            return fragen.ToList();
        }

    }
}
