using Godot;
using System;
using Geek4.scripts.quests;

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

		if (!Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).GetStepFromMilestone(1, 1).StepCompleted||Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).GetStepFromMilestone(1, 2).StepCompleted) return;
		
		Punkty = 0;
		var dialog = new Dialog();
		dialog.AddDialog("Maria", "Muszę zdać ten egzamin", "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddDialog("Maria", "Zacznę od Matematyki", "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Ile wynosi suma 69 + 56?", new()
		{
			{"125", () => { Punkty++;}},
			{"124", () => { }},
			{"123", () => { }}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Jak nazywa się liczba, która jest wynikiem dodawania?", new()
		{
			{"Suma", () => {Punkty++;}},
			{"Różnica", () => {}},
			{"Iloczyn", () => { }}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Ile wynosi kwadrat liczby 5?", new()
		{
			{"6", () => {}},
			{"7", () => {}},
			{"25", () => { Punkty++;}}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Jeśli od liczby 10 odjąć liczbę 4, to ile wynosi różnica?", new()
		{
			{"6", () => { Punkty++;}},
			{"5", () => {}},
			{"4", () => { }}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Jeśli podzielisz liczbę 36 przez 6, to ile wynosi iloraz?", new()
		{
			{"4", () => {}},
			{"5", () => {}},
			{"6", () => { Punkty++; }}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Które działanie matematyczne odwraca działanie dodawania?", new()
		{
			{"Odejmowanie", () => { Punkty++;}},
			{"Mnożenie", () => {}},
			{"Dzielenie", () => { }}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddDialog("Maria", "Teraz czas na język rosyjski", "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Jak nazywa sie alfabet rosyjski?", new()
		{
			{"Grażdzanka", () => { Punkty++;}},
			{"Cyrylica", () => {}},
			{"Kyrillica", () => { }}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Które słowo oznacza \"Kawa\" po rosyjsku?", new()
		{
			{"Молоко", () => {}},
			{"Чай", () => {}},
			{"Кофе", () => { Punkty++; }}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Które słowo oznacza \"Tak\" po rosyjsku?", new()
		{
			{"Да", () => { Punkty++;}},
			{"Нет", () => {}},
			{"Пожалуйста", () => {}}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Jak powiedzieć \"dziękuję\" po rosyjsku?", new()
		{
			{"Да", () => {}},
			{"Спасибо", () => { Punkty++;}},
			{"Привет", () => {}}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Które słowo oznacza \"kot\" po rosyjsku?", new()
		{
			{"Собака", () => {}},
			{"Лошадь", () => {}},
			{"Кот", () => { Punkty++;}}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Które słowo oznacza \"herbata\" po rosyjsku?", new()
		{
			{"Кофе", () => {}},
			{"Чай", () => { Punkty++;}},
			{"Вода", () => {}}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddDialog("Maria", "Pora na ostatni sprawdzian teraz z chemii", "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Który pierwiastek jest oznaczany symbolem \"H\" w układzie okresowym?", new()
		{
			{"Tlen", () => {}},
			{"Węgiel", () => {}},
			{"Wodór", () => { Punkty++;}}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Jaki jest wzór chemiczny na wodę?", new()
		{
			{"CO\u2082", () => {}},
			{"H\u2082O", () => { Punkty++;}},
			{"NaCl", () => {}}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Który gaz jest niezbędny do oddychania?", new()
		{
			{"Tlen", () => { Punkty++;}},
			{"Azot", () => {}},
			{"Wodór", () => {}}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Co oznacza skrót \"NaCl\" w chemii?", new()
		{
			{"Sól kuchenną", () => { Punkty++;}},
			{"Wodę", () => {}},
			{"Węglan sodu", () => {}}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Co oznacza skrót \"CO\" w chemii?", new()
		{
			{"Węglan", () => {}},
			{"Wodę", () => {}},
			{"Tlenek Węgla", () => { Punkty++;}}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddDialog("Maria", "\"Pamiętaj chemiku młody, wlewaj zawsze kwas do wody\"", "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddChooseDialog("Maria", "Jakie bezpieczne postępowanie zaleca się stosować podczas mieszania kwasów z wodą?", new()
		{
			{"Najpierw wlewaj kwas, a następnie wodę", () => {}},
			{"Najpierw wlewaj wodę, a następnie kwas", () => {Punkty++;}},
			{"Wlej kwas i wodę jednocześnie", () => {}}
		}, "res://assets/textures/maria/idle/maria-glowa.png");
		dialog.AddDialog("Maria", "Skończyłam. Czas sprawdzić odpowiedzi", "res://assets/textures/maria/idle/maria-glowa.png",
			() =>
			{
				if (Punkty > 10)
				{
					dialog.AddDialog("Maria", "Udało mi się zdać egzamin!", "res://assets/textures/maria/idle/maria-glowa.png");
					Singleton.QuestHandler.GetQuestInstance(typeof(Edukacja)).CompleteStep(1,2);
				} else
				{
					dialog.AddDialog("Maria", "Nie udało mi się zdać egzaminu. Powinnam spróbować jeszcze raz", "res://assets/textures/maria/idle/maria-glowa.png");
				}
			});
		Singleton.Dialoghandler.PlayDialog(dialog);
	}
}
