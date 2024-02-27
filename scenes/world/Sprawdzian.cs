using Godot;
using System;

public partial class Sprawdzian : Node2D
{
	public singleton Singleton;
	public int Punkty = 0;
	public override void _Ready() {
		Singleton = GetNode<singleton>("/root/Singleton");
	}
	public override void _Process(double delta) {
	}

	public void _on_node_2d_interact()
	{
		Punkty = 0;
		var dialog = new Dialog();
		dialog.AddDialog("Maria", "Muszę zdać ten egzamin");
		dialog.AddDialog("Maria", "Zacznę od Matematyki");
		dialog.AddChooseDialog("Maria", "Ile wynosi suma 69 + 56?", new()
		{
			{"125", () => { Punkty++;}},
			{"124", () => { }},
			{"123", () => { }}
		});
		dialog.AddChooseDialog("Maria", "Jak nazywa się liczba, która jest wynikiem dodawania?", new()
		{
			{"Suma", () => {}},
			{"Różnica", () => { Punkty++;}},
			{"Iloczyn", () => { }}
		});
		dialog.AddChooseDialog("Maria", "Ile wynosi kwadrat liczby 5?", new()
		{
			{"6", () => {}},
			{"7", () => {}},
			{"25", () => { Punkty++;}}
		});
		dialog.AddChooseDialog("Maria", "Jeśli od liczby 10 odjąć liczbę 4, to ile wynosi różnica?", new()
		{
			{"6", () => { Punkty++;}},
			{"5", () => {}},
			{"4", () => { }}
		});
		dialog.AddChooseDialog("Maria", "Jeśli podzielisz liczbę 36 przez 6, to ile wynosi iloraz?", new()
		{
			{"4", () => {}},
			{"5", () => {}},
			{"6", () => { Punkty++; }}
		});
		dialog.AddChooseDialog("Maria", "Które działanie matematyczne odwraca działanie dodawania?", new()
		{
			{"Odejmowanie", () => { Punkty++;}},
			{"Mnożenie", () => {}},
			{"Dzielenie", () => { }}
		});
		dialog.AddDialog("Maria", "Teraz czas na język rosyjski");
		dialog.AddChooseDialog("Maria", "Jak nazywa sie alfabet rosyjski?", new()
		{
			{"Grażdzanką", () => { Punkty++;}},
			{"Cyrylicą", () => {}},
			{"Kyrillicą", () => { }}
		});
		dialog.AddChooseDialog("Maria", "Które słowo oznacza \"Kawa\" po rosyjsku?", new()
		{
			{"Молоко", () => {}},
			{"Чай", () => {}},
			{"Кофе", () => { Punkty++; }}
		});
		dialog.AddChooseDialog("Maria", "Które słowo oznacza \"Tak\" po rosyjsku?", new()
		{
			{"Да", () => { Punkty++;}},
			{"Нет", () => {}},
			{"Пожалуйста", () => {}}
		});
		dialog.AddChooseDialog("Maria", "Jak powiedzieć \"dziękuję\" po rosyjsku?", new()
		{
			{"Да", () => {}},
			{"Спасибо", () => { Punkty++;}},
			{"Привет", () => {}}
		});
		dialog.AddChooseDialog("Maria", "Które słowo oznacza \"kot\" po rosyjsku?", new()
		{
			{"Собака", () => {}},
			{"Лошадь", () => {}},
			{"Кот", () => { Punkty++;}}
		});
		dialog.AddChooseDialog("Maria", "Które słowo oznacza \"herbata\" po rosyjsku?", new()
		{
			{"Кофе", () => {}},
			{"Чай", () => { Punkty++;}},
			{"Вода", () => {}}
		});
		dialog.AddDialog("Maria", "Pora na ostatni sprawdzian teraz z chemii");
		dialog.AddChooseDialog("Maria", "Który pierwiastek jest oznaczany symbolem \"H\" w układzie okresowym?", new()
		{
			{"Tlen", () => {}},
			{"Węgiel", () => {}},
			{"Wodór", () => { Punkty++;}}
		});
		dialog.AddChooseDialog("Maria", "Jaki jest wzór chemiczny na wodę?", new()
		{
			{"CO2", () => {}},
			{"H2O", () => { Punkty++;}},
			{"NaCl", () => {}}
		});
		dialog.AddChooseDialog("Maria", "Który gaz jest niezbędny do oddychania?", new()
		{
			{"Tlen", () => { Punkty++;}},
			{"Azot", () => {}},
			{"Wodór", () => {}}
		});
		dialog.AddChooseDialog("Maria", "Co oznacza skrót \"NaCl\" w chemii?", new()
		{
			{"Sól kuchenną", () => { Punkty++;}},
			{"Wodę", () => {}},
			{"Węglan sodu", () => {}}
		});
		dialog.AddChooseDialog("Maria", "Co oznacza skrót \"CO\" w chemii?", new()
		{
			{"Węglan", () => {}},
			{"Wodę", () => {}},
			{"Tlenek Węgla", () => { Punkty++;}}
		});
		dialog.AddChooseDialog("Maria", "\"Pamiętaj, chemiku, zawsze wlewaj kwas do wody\" - jakie bezpieczne postępowanie zaleca się stosować podczas mieszania kwasów z wodą?", new()
		{
			{"Najpierw wlewaj kwas, a następnie wodę", () => {}},
			{"Najpierw wlewaj wodę, a następnie kwas", () => {Punkty++;}},
			{"Wlej kwas i wodę jednocześnie", () => {}}
		});
		dialog.AddDialog("Maria", "Skończyłam czas sprawdzić odpowiedzi", "res://assets/textures/maria/idle/maria-glowa.png",
			() =>
			{
				if (Punkty > 10)
				{
					dialog.AddDialog("Maria", "Udało mi się zdać egzamin");
				} else
				{
					dialog.AddDialog("Maria", "Nie udało mi się zdać egzaminu. Powinnam spróbować jeszcze raz");
				}
			});
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
