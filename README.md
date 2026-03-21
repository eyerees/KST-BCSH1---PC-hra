# 🛡️ Bramble (Unity)

Tento repozitář obsahuje rozpracovaný školní projekt ve formátu 2D RPG z ptačí perspektivy, vytvořený v enginu **Unity** pomocí jazyka **C#**.

### 📅 Aktuální stav k: 21. 03. 2026

Dnes byly do projektu úspěšně implementovány klíčové prvky pro interakci s herním světem:

* **Top-Down Movement:** Skript pro plynulý pohyb hráče ve všech směrech (osa X a Y) s využitím `Vector2` a `Rigidbody2D`.
* **Animovaný hrdina:** Integrace stažených spritů pro hlavní postavu.
* **Animator Controller:** Nastavení logiky pro přepínání animací (Idle/Walk) podle směru a rychlosti pohybu.

---

### 🛠️ Technické specifikace

| Modul | Popis |
| :--- | :--- |
| **Engine** | Unity 2022.3+ |
| **Skriptování** | C# (Visual Studio / VS Code) |
| **Fyzika** | Rigidbody2D (Interpolace pro plynulý pohyb) |
| **Grafika** | 2D Top-Down Sprite Sheet |

---

### 📂 Struktura souborů

* `PlayerMovement.cs`: Hlavní logika ovládání a vstupu od hráče.
* `Assets/Player/Sprite`: Složka s grafickými podklady hráče.
* `Assets/Player/Animation`: Složka s animacemi hráče.

### 🎮 Ovládání a testování

1.  Otevřete projekt v **Unity Editoru**.
2.  Spusťte hlavní scénu pomocí tlačítka **Play**.
3.  **Pohyb:** Použijte klávesy `WASD` nebo `šipky` pro pohyb hráče po mapě.
4.  Postava se automaticky natáčí nebo mění animaci podle směru chůze.

---

**Autor:** Astrid Hendrychová 
**Projekt:** BCSH1 – Volba tématu semestrální práce – (b) Jednoduchá počítačová hra 

---
