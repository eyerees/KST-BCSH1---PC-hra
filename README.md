# 🛡️ Bramble (Unity)

Tento repozitář obsahuje rozpracovaný školní projekt ve formátu 2D RPG z ptačí perspektivy, vytvořený v enginu **Unity** pomocí jazyka **C#**.

### 📅 Aktuální stav k: 13. 04. 2026

Dnes byly do projektu úspěšně implementovány klíčové prvky pro interakci s herním světem:

* **Top-Down Movement:** Skript pro plynulý pohyb hráče ve všech směrech (osa X a Y) s využitím `Vector2` a `Rigidbody2D`.
* **Animovaný hrdina:** Integrace stažených spritů pro hlavní postavu.
* **Animator Controller:** Nastavení logiky pro přepínání animací (Idle/Walk/Attack) podle směru a stavu hráče.
* **Combat System:** Implementován útočný mechanizmus aktivovaný klávesou `Space`.
* **UI Interface:** Přidáno uživatelské rozhraní pro správu inventáře, mapy a stavových ukazatelů, dostupné přes klávesu `Tab`.

---

### 🛠️ Technické specifikace

| Sloupec | Modul | Popis |
| :--- | :--- | :--- |
| 1 | **Engine** | Unity 2022.3+ |
| 2 | **Skriptování** | C# (Visual Studio / VS Code) |
| 3 | **Fyzika** | Rigidbody2D (Interpolace pro plynulý pohyb) |
| 4 | **Grafika** | 2D Top-Down Sprite Sheet |
| 5 | **UI/HUD** | Canvas systém (Inventory, Map, Stats) |

---

### 📂 Struktura souborů

* `PlayerMovement.cs`: Logika pohybu hráče.
* `PlayerCombat.cs`: Logika útoků a detekce zásahu.
* `UIManager.cs`: Správa prvků uživatelského rozhraní a menu.
* `Assets/Player/Sprite`: Složka s grafickými podklady.
* `Assets/Player/Animation`: Složka s animacemi.

### 🎮 Ovládání a testování

1.  Otevřete projekt v **Unity Editoru**.
2.  Spusťte hlavní scénu pomocí tlačítka **Play**.
3.  **Pohyb:** Použijte klávesy `WASD` nebo `šipky`.
4.  **Útok:** Stiskněte `Space` pro aktivaci útoku.
5.  **UI Menu:** Stiskněte `Tab` pro zobrazení inventáře a mapy apod.

---

**Autor:** Astrid Hendrychová 
**Projekt:** BCSH1 – Volba tématu semestrální práce – (b) Jednoduchá počítačová hra
