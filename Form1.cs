namespace Prsi_hra
{
	public partial class Form1 : Form
	{
		/// <summary>
		/// Výèet Stav pøedstavuje rùzné stavy bìhem hry.
		/// </summary>
		enum Stav { Start, Hra, Konec, TahHrace, TahPc }

		/// <summary>
		/// Výèet NaTahu pøedstavuje, kdo je na tahu.
		/// </summary>
		enum NaTahu { Hrac, Pocitac }

		/// <summary>
		/// Urèuje, kdo je aktuálnì na tahu.
		/// </summary>
		NaTahu naTahu;

		/// <summary>
		/// Poèet karet, který se vejde na šíøku okna.
		/// </summary>
		const int N = 8;

		/// <summary>
		/// Poèet karet, se kterými se má hrát.
		/// </summary>
		int pocetKaret = 4;

		/// <summary>
		/// Unicode znaky pro barvy karet.
		/// </summary>
		string kuleZnak = "\u2666";
		string srdceZnak = "\u2665";
		string listyZnak = "\u2660";
		string zaludyZnak = "\u2663";

		/// <summary>
		/// Stringy s èeskou diakritikou pro hodnoty karet.
		/// </summary>
		string eso = "Eso";
		string spodek = "Spodek";
		string svrsek = "Svršek";
		string kral = "Král";

		/// <summary>
		/// Balíèek, ze kterého se tahají karty.
		/// </summary>
		List<(string hodnoty, string barvy)> tahaciBalicek = new List<(string, string)>();
		List<(string hodnoty, string barvy)> balicekProOdkladani = new List<(string, string)>();
		List<(string hodnoty, string barvy)> kartyHrace = new List<(string, string)>();
		List<(string hodnoty, string barvy)> kartyPocitace = new List<(string, string)>();

		/// <summary>
		/// Karty hráèe jako tlaèítka.
		/// </summary>
		List<Button> kartyTlacitka = new List<Button>();

		/// <summary>
		/// Barvy karet.
		/// </summary>
		List<string> barvy = new List<string>();

		/// <summary>
		/// Karta na vrchu balíèku.
		/// </summary>
		(string hodnoty, string barvy) kartaNaVrchu;

		/// <summary>
		/// Poèet použitých sedm.
		/// </summary>
		int sedmaPouzita = 0;

		/// <summary>
		/// Poèet použitých es.
		/// </summary>
		int esaPouzita = 0;

		/// <summary>
		/// Barva, na kterou nìkdo zmìnil barvu hry.
		/// </summary>
		string zmenenaBarva = "";

		/// <summary>
		/// Zda hráè hrál Svršek a musí si vybrat barvu.
		/// </summary>
		bool vyberBarvy = false;

		/// <summary>
		/// Zda došly karty a musí se ukonèit hra.
		/// </summary>
		bool doslyKarty = false;

		/// <summary>
		/// Sedma srdcová karta.
		/// </summary>
		(string hodnoty, string barvy) sedmaSrdcova;

		/// <summary>
		/// Generátor náhodných èísel.
		/// </summary>
		Random random = new Random();

		/// <summary>
		/// Inicializuje novou instanci tøídy <see cref="Form1"/>.
		/// </summary>
		public Form1()
		{
			InitializeComponent();
			NastavStav(Stav.Start);
			naTahu = NaTahu.Hrac;
			sedmaSrdcova = ("7", srdceZnak);

			barvy.Add(kuleZnak);
			barvy.Add(srdceZnak);
			barvy.Add(listyZnak);
			barvy.Add(zaludyZnak);
		}


		//nastavuje zpravu, kolik pocitaci zbyva karet + ceske sklonovani
		void NastavPocetKaretPocitace()
		{
			if (kartyPocitace.Count == 1)
			{
				pocet_karet_pocitace.Text = "Zbývá mi poslední karta!!!";
			}
			else if (kartyPocitace.Count <= 4)
			{
				pocet_karet_pocitace.Text = $"Zbývají mi {kartyPocitace.Count} karty.";
			}
			else
			{
				pocet_karet_pocitace.Text = $"Zbývá mi {kartyPocitace.Count} karet.";
			}
		}

		//tahani karty, stastlivci se prida karta, ktera se z tahaciho balicku se odstrani
		void TahejKartu(List<(string, string)> tahajici)
		{
			//z balicku pro odkladani udelame balicek pro tahani
			if (tahaciBalicek.Count == 0)
			{
				balicekProOdkladani.RemoveAt(balicekProOdkladani.Count - 1);
				balicekProOdkladani.Reverse();
				tahaciBalicek = balicekProOdkladani.Concat(tahaciBalicek).ToList();
				balicekProOdkladani.Clear();
				balicekProOdkladani.Add(kartaNaVrchu);
			}

			//nelze-li tahat, konci hra
			if (tahaciBalicek.Count == 0 && balicekProOdkladani.Count == 1)
			{
				doslyKarty = true;
			}
			else
			{
				tahajici.Add(tahaciBalicek.Last());
				tahaciBalicek.RemoveAt(tahaciBalicek.Count - 1);
			}
		}

		//logika pocitace, pocitac se rozhodne pro nejaky krok a provede ho
		void PocitaciHraj(ref bool hracMusiStat)
		{
			//muze predcasne ukoncit generovani
			bool pokracovat = true;

			//musi hledat v hodnotach karet drive nez v barvach
			bool zacitHodnotami = false;
			string tempZmenenaBarva = zmenenaBarva;

			//musi hrat na sedmu
			if (SedmaNaVrchu() && sedmaPouzita != 0)
			{
				bool obsahujeSedmu = false;

				//hleda u sebe sedmu
				for (int i = 0; i < kartyPocitace.Count; i++)
				{
					if (kartyPocitace[i].hodnoty == "7")
					{
						obsahujeSedmu = true;
						break;
					}
				}

				//nema-li sedmu, taha karty
				if (!obsahujeSedmu)
				{
					if (2 * sedmaPouzita <= 4)
					{
						zprava.Text = string.Format("Tahal jsem si {0} karty.", 2 * sedmaPouzita);
					}
					else
					{
						zprava.Text = string.Format("Tahal jsem si {0} karet.", 2 * sedmaPouzita);
					}

					for (int i = 0; i < 2 * sedmaPouzita; i++)
					{
						TahejKartu(kartyPocitace);
					}

					//resretuje zahrani sedmy
					sedmaPouzita = 0;
					return;
				}

				//ma-li, hleda v hodnotach
				else
				{
					zacitHodnotami = true;
				}
			}

			//musi hrat na eso
			else if (EsoNaVrchu() && esaPouzita != 0)
			{
				bool obshujeEso = false;

				//hleda u sebe eso
				for (int i = 0; i < kartyPocitace.Count; i++)
				{
					if (kartyPocitace[i].hodnoty == "Eso")
					{
						obshujeEso = true;
						break;
					}
				}

				//nema-li, stoji
				if (!obshujeEso)
				{
					zprava.Text = "Toto kolo stojím!";

					//reset
					esaPouzita = 0;
					return;
				}

				//bude hledat v hodnotach
				else
				{
					zacitHodnotami = true;
				}
			}

			//byl-li zahran Svrsek 
			else if (SvrsekNaVrchu() && zmenenaBarva != "")
			{
				bool obsahujeZmenenouBarvu = false;

				//hleda kartu s novou barvou
				for (int i = 0; i < kartyPocitace.Count; i++)
				{
					if (kartyPocitace[i].barvy == zmenenaBarva)
					{
						obsahujeZmenenouBarvu = true;
						zmenenaBarva = "";
						zmenena_barva.Text = "Barva nebyla zmìnìna!";
						break;
					}
				}

				//hleda Svrsek, ten muze zahrat kdykoliv
				bool obsahujeSvrsek = false;
				for (int i = 0; i < kartyPocitace.Count; i++)
				{
					if (kartyPocitace[i].hodnoty == svrsek)
					{
						obsahujeSvrsek = true;
						zmenenaBarva = "";
						break;
					}
				}

				//nema-li zadnou z nich, tak taha kartu
				if (!(obsahujeZmenenouBarvu || obsahujeSvrsek))
				{
					TahejKartu(kartyPocitace);
					zprava.Text = "Tahal jsem kartu!";
					return;
				}
			}

			//rika, kterou barvu ma hledat
			string hledanaBarva = "";

			//neni-li zmenena barva, hleda barvu karty na vrchu
			if (tempZmenenaBarva == "")
			{
				hledanaBarva = kartaNaVrchu.barvy;
			}

			//jinak hleda barvu, na kterou se menilo, hleda temp, protoze promenna zmenenaBarva mohla byt prepsana
			else
			{
				hledanaBarva = tempZmenenaBarva;
			}

			//indexy karet, na kterych se nachazi
			int indexSedmy = -1;
			int indexEsa = -1;

			//ma-li hrac posledni kartu, tak se pocitac snazi zahrat budto Sedmu nebo Eso
			if (kartyHrace.Count == 1 && kartaNaVrchu.hodnoty != "7" && kartaNaVrchu.hodnoty != "Eso")
			{
				for (int i = 0; i < kartyPocitace.Count; ++i)
				{
					if (kartyPocitace[i].hodnoty == "7" && kartyPocitace[i].barvy == hledanaBarva)
					{
						indexSedmy = i;
						break;
					}
					if (kartyPocitace[i].hodnoty == eso && kartyPocitace[i].barvy == hledanaBarva)
					{
						indexEsa = i;
					}
				}
			}

			//nasledujici radky jsou pro hledani v kartach
			if (pokracovat && indexSedmy != -1)
			{
				//hledani Sedmy
				HledaniVKartachPc("hodnoty", hledanaBarva, ref pokracovat, "Sedma");
			}

			if (pokracovat && indexEsa != -1)
			{
				//hledani Esa
				HledaniVKartachPc("hodnoty", hledanaBarva, ref pokracovat, "Eso");
			}

			if (pokracovat && !zacitHodnotami)
			{
				//hledani v barvach
				HledaniVKartachPc("barvy", hledanaBarva, ref pokracovat);
			}

			if (pokracovat)
			{
				//hledani v hodnotach
				HledaniVKartachPc("hodnoty", hledanaBarva, ref pokracovat);
			}

			if (pokracovat)
			{
				//hledani Svrsku
				HledaniVKartachPc("hodnoty", hledanaBarva, ref pokracovat, svrsek);
			}

			if (pokracovat)
			{
				//nakonec, pokud nelze nic zahrat, taha kartu
				TahejKartu(kartyPocitace);
				zprava.Text = "Tahal jsem kartu!";
			}

			bool hracMaEso = false;
			bool hracMaSedmu = false;
			bool pocitacHralEso = EsoNaVrchu() && esaPouzita != 0;
			bool pocitacHralSedmu = SedmaNaVrchu() && sedmaPouzita != 0;

			//kdyz pocitac zahraje Sedmu nebo Eso, tak hrac musi na tyto karty reagovat
			if (pocitacHralEso || pocitacHralSedmu)
			{
				for (int i = 0; i < kartyHrace.Count; i++)
				{
					if (kartyHrace[i].hodnoty == eso && pocitacHralEso)
					{
						//muze-li hrac Eso
						hracMaEso = true;
						zprava.Text = "Zahrál jsem Eso, pøebijte ho nebo stujte!";
						tahat_karty.Text = "stát";
					}
					else if (kartyHrace[i].hodnoty == "7" && pocitacHralSedmu)
					{
						//muze-li prebit Sedmu
						hracMaSedmu = true;
						zprava.Text = "Zahrál jsem Sedmu, pøebijte ji nebo tahejte karty!";

					}
				}

				//nemuze-li prebit
				if (!hracMaEso && pocitacHralEso)
				{
					zprava.Text = "Zahrál jsem Eso, nemùžete pøebíjet, stojíte! Kliknìte pokraèovat!";
					tahat_karty.Text = "pokraèovat";
					hracMusiStat = true;
				}

				else if (!hracMaSedmu && pocitacHralSedmu) 
				{
					zprava.Text = "Zahrál jsem Sedmu, nemùžete ji pøebít! Tahejte karty!";
				}
			}
		}

		//vytvori karticky a prida je do balicku karet
		void VytvorKarticky(ref List<(string, string)> balicekKaret, List<string> barvy)
		{
			foreach (var barva in barvy)
			{
				for (int hodnota = 7; hodnota <= 10; hodnota++)
				{
					balicekKaret.Add((hodnota.ToString(), barva));
				}

				balicekKaret.Add((spodek, barva));
				balicekKaret.Add((svrsek, barva));
				balicekKaret.Add((eso, barva));
				balicekKaret.Add((kral, barva));
			}
		}

		//Fisher-Yates algoritmus pro zamichani balicku
		void ZamichejKarty(List<(string, string)> balicekKaret)
		{
			int pocetKaretVBalicku = balicekKaret.Count;
			while (pocetKaretVBalicku > 1)
			{
				pocetKaretVBalicku--;
				int k = random.Next(pocetKaretVBalicku + 1);
				var value = balicekKaret[k];
				balicekKaret[k] = balicekKaret[pocetKaretVBalicku];
				balicekKaret[pocetKaretVBalicku] = value;
			}
		}

		//vytvori karty hrace
		void VytvorHraciKarty(int vyskaKarty, int sirkaKarty, int pocetKaret, List<(string, string)> balicekKaret, List<Button> tlacitka, List<(string, string)> kartyHrace)
		{
			for (int i = 0; i < N; i++)
			{
				for (int j = N / 2; j < N; j++)
				{
					Button b = new Button();
					b.Font = new Font(b.Font.FontFamily, 11);
					b.Height = vyskaKarty;
					b.Width = sirkaKarty;
					b.Left = i * sirkaKarty;
					b.Top = j * vyskaKarty;
					b.Parent = this;
					tlacitka.Add(b);
					b.Click += Karticka_Click;//pridame metodu Karticka_Click
					b.Visible = false;
					if (pocetKaret > 0)
					{
						//zobrazime jen neprazdne karty
						(string hodnoty, string barvy) karticka = balicekKaret.Last();

						b.Text = string.Join(" ", karticka.hodnoty, karticka.barvy);
						b.Tag = karticka;
						TahejKartu(kartyHrace);
						b.Visible = true;
					}
					pocetKaret--;
				}
			}
		}

		/// <summary>
		/// Rozdá karty pro poèítaè.
		/// </summary>
		/// <param name="pocetKaret">Poèet karet, které mají být rozdány.</param>
		/// <returns>Seznam rozdaných karet.</returns>
		/// <remarks>
		/// Tato metoda vytváøí seznam karet a volá metodu <see cref="TahejKartu"/> pro každou kartu.
		/// </remarks>
		List<(string, string)> RozdejKarty(int pocetKaret)
		{
			List<(string, string)> rozdaneKarty = new List<(string, string)>();

			for (int i = 0; i < pocetKaret; i++)
			{
				TahejKartu(rozdaneKarty);
			}
			return rozdaneKarty;
		}

		/// <summary>
		/// Zjistí, zda lze hrát první kartu na druhou kartu.
		/// </summary>
		/// <param name="co">Karta, která má být hrána.</param>
		/// <param name="sCim">Karta, na kterou má být hrána.</param>
		/// <returns>Vrací true, pokud lze kartu hrát, jinak false.</returns>
		/// <remarks>
		/// Tato metoda porovnává hodnoty a barvy karet. Pokud byl zahrán Svršek, porovnává se se zmìnìnou barvou.
		/// </remarks>
		bool HratelnaKarta((string hodnoty, string barvy) co, (string hodnoty, string barvy) sCim)
		{
			if (zmenenaBarva != "")
			{
				// byl-li zahrán Svršek, musíme porovnávat se zmìnìnou barvou
				return co.hodnoty == sCim.hodnoty || co.barvy == zmenenaBarva || co.hodnoty == svrsek;
			}

			// jinak klasicky dvì karty
			return co.hodnoty == sCim.hodnoty || co.barvy == sCim.barvy || co.hodnoty == svrsek;
		}


		//3 funkce nize slouzi pro zjisteni, zda je na vrchu dana hodnota
		bool SedmaNaVrchu()
		{
			return kartaNaVrchu.hodnoty == "7";
		}

		bool EsoNaVrchu()
		{
			return kartaNaVrchu.hodnoty == eso;
		}

		bool SvrsekNaVrchu()
		{
			return kartaNaVrchu.hodnoty == svrsek;
		}

		
		void PolozitKartuNaVrch((string hodnoty, string barvy) karta, ref List<(string, string)> balicek, int indexOdebrani)
		{
			kartaNaVrchu = karta;
			balicekProOdkladani.Add(kartaNaVrchu);
			horni_karta.Text = string.Join(" ", kartaNaVrchu.hodnoty, kartaNaVrchu.barvy);
			balicek.RemoveAt(indexOdebrani);//odebere se z balicku toho, kdo kartu zahral
		}

		//zobrazeni predchozi karty
		void NastavPredchoziKartu((string hodnoty, string barvy) karta)
		{
			predchozi_karta.Text = $"pøedchozí karta: {karta.hodnoty} {karta.barvy}";
		}

		//implementace pravidla - sedma sedcova vraci do hry
		void SedmaSrdcovaVraciDoHry(ref List<(string, string)> prohledavanyBalicek, ref List<(string hodnoty, string barvy)> kartyVracejicihoSe)
		{
			prohledavanyBalicek.Remove(sedmaSrdcova);//u toho, kdo ma kartu
			NastavPredchoziKartu(kartaNaVrchu);
			kartaNaVrchu = sedmaSrdcova;
			horni_karta.Text = string.Join(" ", kartaNaVrchu.hodnoty, kartaNaVrchu.barvy);
			balicekProOdkladani.Add((kartaNaVrchu));

			for (int i = 0; i < 2 * sedmaPouzita + 2; i++)
			{
				//kdo se vraci, tak si taha prislusny pocet karet
				TahejKartu(kartyVracejicihoSe);
			}

			sedmaPouzita = 0;//reset
			if (kartyVracejicihoSe == kartyHrace)
			{
				//vraci-li se hrac
				NastavStav(Stav.TahHrace);
				zprava.Text = "Sedma srdcová vás vrátila zpìt do hry, kliknìte pokraèovat!";
				konec_zprava.Text = "pokraèovat";
			}
			else
			{
				//jinak
				zprava.Text = "Vaše Sedma srdcová mì vrátila do hry!";
				NastavPocetKaretPocitace();
				NastavStav(Stav.TahHrace);
			}
		}

		//pro pc, prohledavani jeho karet
		void HledaniVKartachPc(string cimZacit, string hledanaBarva, ref bool pokracovat, string specifikovane = "")
		{
			string hledanyPrvek = "";//prvek, ktery chceme hledat - hodnota nebo barva

			if (cimZacit == "barvy")
			{
				hledanyPrvek = hledanaBarva;
			}
			else if (cimZacit == "hodnoty")
			{
				if (specifikovane != "")
				{
					//specifikovali jsme-li, co chceme hledat, tak to nastavime
					hledanyPrvek = specifikovane;
				}
				else
				{
					//jinak chceme hledat hodnotu z vrchu
					hledanyPrvek = kartaNaVrchu.hodnoty;
				}
			}

			for (int i = 0; i < kartyPocitace.Count; i++)
			{
				string aktualniPrvek;//aktualni prvek z karet pc, na ktery se koukame
				if (cimZacit == "barvy")
				{
					//postupne ho nastavujeme
					aktualniPrvek = kartyPocitace[i].barvy;
				}
				else
				{
					aktualniPrvek = kartyPocitace[i].hodnoty;
				}

				//najdeme-li hledany prvek
				if (hledanyPrvek == aktualniPrvek)
				{
					NastavPredchoziKartu(kartaNaVrchu);
					PolozitKartuNaVrch(kartyPocitace[i], ref kartyPocitace, i);
					
					//nebudeme dale hledat
					pokracovat = false;
					
					//zahrali jsme-li nejakou specialni kartu, tak zvysime pocet zahrani teto karty
					if (kartaNaVrchu.hodnoty == "7")
					{
						sedmaPouzita += 1;
					}
					else if (kartaNaVrchu.hodnoty == eso)
					{
						esaPouzita += 1;
					}
					else if (kartaNaVrchu.hodnoty == svrsek && kartyPocitace.Count == 0)
					{
						//v pripade zahrani Svrsku jako posledni karty, nastavime novou barvu vzdy na kule
						zmenenaBarva = kuleZnak;
						zprava.Text = $"Zahral jsem Svrska a zmenil barvu na {zmenenaBarva}!";
						zmenena_barva.Text = $"Barva je zmìnìna na {zmenenaBarva}";
					}
					else if (kartaNaVrchu.hodnoty == svrsek && kartyPocitace.Count != 0)
					{
						//zahraje-li se Svrsek a neni to posledni karta
						for (int j = 0; j < kartyPocitace.Count; j++)
						{
							//chceme nastavit novou barvu odlisnou od te na vrchu a pokousime se nezahrat Svrsek
							if (kartyPocitace[j].barvy != kartaNaVrchu.barvy && kartyPocitace[j].hodnoty != svrsek)
							{
								zmenenaBarva = kartyPocitace[j].barvy;
							}
						}
						
						//pokud to nelze, zahrajeme posledni kartu z balicku
						if (zmenenaBarva == "")
						{
							zmenenaBarva = kartyPocitace[kartyPocitace.Count - 1].barvy;
						}

						zprava.Text = $"Zahrál jsem Svrška a zmìnil barvu na {zmenenaBarva}!";
						zmenena_barva.Text = $"Barva je zmìnìna na {zmenenaBarva}";
					}
					else
					{
						//nezahrali jsme-li zadnou specialni kartu, zobrazime tento text
						zprava.Text = "Zahrál jsem kartu!";
					}

					break;
				}
			}
		}

		//pro hrace - nastaveni nove barvy po zahrani Svrska
		void NastavitNovouBarvu(string barva)
		{
			if (vyberBarvy && naTahu == NaTahu.Hrac)
			{
				vyberBarvy = false;
				zmenenaBarva = barva;
				zmenena_barva.Text = $"Barva je zmìnìna na {zmenenaBarva}";
				naTahu = NaTahu.Pocitac;
				NastavStav(Stav.TahPc);
			}
		}

		void NastavStav(Stav novy)
		{
			//zmeneni stavu a vzhledu okna
			switch (novy)
			{
				case Stav.Start://na zacatku, uvodni obrazovka
					naTahu = NaTahu.Hrac;
					pocatecni_pocet_karet.Visible = true;
					b4.Visible = true;
					b5.Visible = true;
					b6.Visible = true;
					prsi.Visible = true;

					pocet_karet_pocitace.Visible = false;
					zmenena_barva.Visible = false;
					horni_karta.Visible = false;
					tahat_karty.Visible = false;
					zprava.Visible = false;
					konec_zprava.Visible = false;
					hrat_znova.Visible = false;
					vase_karty.Visible = false;
					popisek_karta_na_vrchu.Visible = false;

					kule_tlacitko.Visible = false;
					kule_tlacitko.Text = kuleZnak;
					srdce_tlacitko.Visible = false;
					srdce_tlacitko.Text = srdceZnak;
					listy_tlacitko.Visible = false;
					listy_tlacitko.Text = listyZnak;
					zaludy_tlacitko.Visible = false;
					zaludy_tlacitko.Text = zaludyZnak;
					predchozi_karta.Visible = false;

					break;

				case Stav.Hra://priprava hry
					pocatecni_pocet_karet.Visible = false;
					b4.Visible = false;
					b5.Visible = false;
					b6.Visible = false;
					prsi.Visible = false;

					pocet_karet_pocitace.Visible = true;
					zmenena_barva.Visible = true;
					horni_karta.Visible = true;
					tahat_karty.Visible = true;
					zprava.Text = "Hrajete!";
					zprava.Visible = true;
					predchozi_karta.Visible = true;
					vase_karty.Visible = true;
					popisek_karta_na_vrchu.Visible = true;

					kule_tlacitko.Visible = true;
					srdce_tlacitko.Visible = true;
					listy_tlacitko.Visible = true;
					zaludy_tlacitko.Visible = true;

					int sirkaKarty = ClientSize.Width / N;
					int vyskaKarty = ClientSize.Height / N;
					VytvorKarticky(ref tahaciBalicek, barvy);

					ZamichejKarty(tahaciBalicek);

					predchozi_karta.Text = "pøedchozí karta:";//na zacatku neni predchozi karta
					PolozitKartuNaVrch(tahaciBalicek.Last(), ref tahaciBalicek, tahaciBalicek.Count - 1);

					kartyPocitace = RozdejKarty(pocetKaret);

					VytvorHraciKarty(vyskaKarty, sirkaKarty, pocetKaret, tahaciBalicek, kartyTlacitka, kartyHrace);

					NastavPocetKaretPocitace();

					break;

				//po tom, co hrac ukonci svuj tah, tak chceme aktualizovat karty, ktere ma hrac v ruce
				case Stav.TahHrace:
					for (int i = 0; i < kartyHrace.Count; i++)
					{
						//prepisi se hodnoty tlacitek na aktualni
						kartyTlacitka[i].Text = string.Join(" ", kartyHrace[i].hodnoty, kartyHrace[i].barvy);
						kartyTlacitka[i].Tag = kartyHrace[i];
						kartyTlacitka[i].Visible = true;
					}
					
					//pokud je nejake tlacitko navic, tj. z predchoziho tahu, tak ho zneviditelnime
					int posledni = kartyHrace.Count;
					while (kartyTlacitka[posledni].Tag != null)
					{
						kartyTlacitka[posledni].Tag = null;
						kartyTlacitka[posledni].Visible = false;
						posledni++;
					}

					if (!vyberBarvy)
					{
						naTahu = NaTahu.Pocitac;
					}
					break;

				case Stav.TahPc:
					bool hracMusiStat = false;//pokud hrac nemuze prebit Eso musi stat, zkontroluje se po tahu pocitace

					if (doslyKarty)
					{
						konec_zprava.Text = "        Došly karty!\n       Konec hry!";
						NastavStav(Stav.Konec);
						break;
					}

					PocitaciHraj(ref hracMusiStat);

					if (kartyHrace.Count == 0)
					{
						//hraci dosly karty
						if (kartyPocitace.Contains(sedmaSrdcova) && (SedmaNaVrchu() || kartaNaVrchu.hodnoty == srdceZnak))
						{
							//lze-li aplikovat pravidlo sedmy srdcove, tak se hrac vraci do hry
							SedmaSrdcovaVraciDoHry(ref kartyPocitace, ref kartyHrace);
						}
						else
						{
							//jinak vyhral
							konec_zprava.Text = "        Vyhrál jste!\n        Konec hry!";
							NastavStav(Stav.Konec);
							break;
						}
					}

					if (kartyPocitace.Count != 0)
					{
						//ma-li pocitac jeste nejake karty
						NastavPocetKaretPocitace();
					}
					else
					{
						//jiz nema zadne
						if (kartyHrace.Contains(sedmaSrdcova) && (SedmaNaVrchu() || kartaNaVrchu.barvy == srdceZnak))
						{
							SedmaSrdcovaVraciDoHry(ref kartyHrace, ref kartyPocitace);
						}
						else
						{
							konec_zprava.Text = "Byl/a jste poražen/a.\nKonec hry!";
							NastavStav(Stav.Konec);
							break;
						}
					}

					if (hracMusiStat)
					{
						//zahral-li pocitac Eso a hrac ho nemuze prebit, tak stoji
						break;
					}
					naTahu = NaTahu.Hrac;
					break;

				case Stav.Konec:
					konec_zprava.Visible = true;
					hrat_znova.Visible = true;

					pocet_karet_pocitace.Visible = false;
					zmenena_barva.Visible = false;
					horni_karta.Visible = false;
					tahat_karty.Visible = false;
					zprava.Visible = false;
					predchozi_karta.Visible = false;
					vase_karty.Visible = false;
					popisek_karta_na_vrchu.Visible = false;

					kule_tlacitko.Visible = false;
					srdce_tlacitko.Visible = false;
					listy_tlacitko.Visible = false;
					zaludy_tlacitko.Visible = false;

					for (int i = 0; i < kartyHrace.Count; i++)
					{
						kartyTlacitka[i].Visible = false;
					}
					
					//vse se vymaze
					kartyTlacitka.Clear();
					tahaciBalicek.Clear();
					balicekProOdkladani.Clear();
					kartyHrace.Clear();
					kartyPocitace.Clear();
					
					//nastavi se vychozi hodnoty promennych
					sedmaPouzita = 0;
					esaPouzita = 0;
					zmenenaBarva = "";
					vyberBarvy = false;
					doslyKarty = false;

					break;

				default:
					break;
			}
		}

		private void Karticka_Click(object? sender, EventArgs e)
		{
			//chovani pri kliknuti karty hrace
			Button? kliknuteTlacitko = sender as Button;

			(string hodnoty, string barvy) tagHodnota;
			if (kliknuteTlacitko == null || kliknuteTlacitko.Tag == null)
			{
				return;
			}
			
			//hodnota karticky
			tagHodnota = ((string hodnoty, string barvy))kliknuteTlacitko.Tag;

			if (SedmaNaVrchu() && sedmaPouzita != 0 && tagHodnota.hodnoty != "7")
			{
				//pokud hrac musi hrat Sedmu, ale neklikne na ni
				return;
			}
			else if (EsoNaVrchu() && esaPouzita != 0 && tagHodnota.hodnoty != eso)
			{
				//musi-li hrat Eso a nevybere ji
				return;
			}
			
			//lze-li hrat kliknutou kartu
			if (naTahu == NaTahu.Hrac && HratelnaKarta(tagHodnota, kartaNaVrchu) && !vyberBarvy)
			{
				tahat_karty.Text = "tahat karty";
				zmenenaBarva = "";//resetuje se zmenena barva
				zmenena_barva.Text = "Barva nebyla zmìnìna!";

				for (int i = 0; i < kartyHrace.Count; i++)
				{
					//najde se polozena karta
					if (kartyHrace[i].hodnoty == tagHodnota.hodnoty && kartyHrace[i].barvy == tagHodnota.barvy)
					{
						
						if (kartyHrace[i].hodnoty == svrsek)
						{
							//je-li to Svrsek, hrac vybira barvu
							vyberBarvy = true;
							zprava.Text = "Vyberte novou barvu!";

						}
						NastavPredchoziKartu(kartaNaVrchu);
						PolozitKartuNaVrch(kartyHrace[i], ref kartyHrace, i);

						break; 
					}
				}

				if (kartaNaVrchu.hodnoty == "7")
				{
					sedmaPouzita += 1;
				}
				else if (kartaNaVrchu.hodnoty == eso)
				{
					esaPouzita += 1;
				}

				if (zmenenaBarva == "")
				{
					//nebyla-li zmenemna barva
					zmenena_barva.Text = "Barva nebyla zmìnìna!";
				}

				NastavStav(Stav.TahHrace);

				if (vyberBarvy && kartyHrace.Count != 0)
				{
					//pokud si ma hrac vybrat barvu a zbyvaji mu nejake karty
					return;
				}

				NastavStav(Stav.TahPc);
			}
		}

		private void b4_Click(object sender, EventArgs e)
		{
			pocetKaret = 4;
			NastavStav(Stav.Hra);
		}

		private void b5_Click(object sender, EventArgs e)
		{
			pocetKaret = 5;
			NastavStav(Stav.Hra);
		}

		private void b6_Click(object sender, EventArgs e)
		{
			pocetKaret = 6;
			NastavStav(Stav.Hra);
		}

		private void tahat_karty_Click(object sender, EventArgs e)
		{
			if (kartaNaVrchu == sedmaSrdcova && tahat_karty.Text == "pokraèovat")
			{
				//hrac je vracen do hry pomoci Sedmy srdcove
				konec_zprava.Text = "tahat karty";
				NastavStav(Stav.TahHrace);
				NastavStav(Stav.TahPc);
			}
			else if (SedmaNaVrchu() && naTahu == NaTahu.Hrac && sedmaPouzita != 0)
			{
				//tahani karet po zahrani Sedmy
				tahat_karty.Text = "tahat karty";
				for (int i = 0; i < 2 * sedmaPouzita; i++)
				{
					TahejKartu(kartyHrace);
				}
				sedmaPouzita = 0;
				NastavStav(Stav.TahHrace);
				NastavStav(Stav.TahPc);
			}
			else if (EsoNaVrchu() && new[] { "pokraèovat", "stát" }.Contains(tahat_karty.Text) && esaPouzita != 0)
			{
				//hrac stoji po zahrani esa pocitacem
				tahat_karty.Text = "tahat karty";
				esaPouzita = 0;
				NastavStav(Stav.TahPc);
			}
			else if (naTahu == NaTahu.Hrac && !vyberBarvy)
			{
				//tahani jedne karty
				tahat_karty.Text = "tahat karty";
				sedmaPouzita = 0;
				esaPouzita = 0;
				TahejKartu(kartyHrace);
				NastavStav(Stav.TahHrace);
				NastavStav(Stav.TahPc);
			}
		}

		private void kule_Click(object sender, EventArgs e)
		{
			NastavitNovouBarvu(kuleZnak);
		}

		private void srdce_Click(object sender, EventArgs e)
		{
			NastavitNovouBarvu(srdceZnak);
		}

		private void listy_Click(object sender, EventArgs e)
		{
			NastavitNovouBarvu(listyZnak);
		}

		private void zaludy_Click(object sender, EventArgs e)
		{
			NastavitNovouBarvu(zaludyZnak);
		}

		private void hrat_znova_Click(object sender, EventArgs e)
		{
			NastavStav(Stav.Start);
		}
	}
}