# Fandango (Unity)

Tento repozitář obsahuje rozpracovaný školní projekt ve formátu 2D RPG z ptačí perspektivy, vytvořený v enginu **Unity** pomocí jazyka **C#**.

### 📅 Aktuální stav k: 15. 04. 2026

* **Main Menu**
* **User Interface**
* **Dialogue System**
* **Quest System** 
* **Combat System**
* **3 Lokace**
* **SFX**

---

### 🛠️ Technické specifikace

| Sloupec | Modul | Popis |
| :--- | :--- | :--- |
| 1 | **Engine** | Unity 6 (6000.3.10f1) |
| 2 | **Skriptování** | C# (VS Code) |
| 3 | **Fyzika** | Rigidbody2D |
| 4 | **Grafika** | 2D Top-Down Sprite Sheet + Tilemap |
| 5 | **Systémy** | Quest, Dialogue, Combat, UI |

---

### 📂 Klíčová struktura projektu

* `Assets/Prefabs/`: Obsahuje herní objekty (Enemies, Item, UI).
* `Assets/Scripts/`: Logika rozdělená do modulů (Effects, Entities, Interaction, Items, System, UI).
* `Assets/Tileset/`: Definice prostředí (Ground, Structure, Tree, Wall, etc.).
* `Assets/ScriptableObjects/`: Data pro NPC a úkoly (Quest).

### 🎮 Ovládání a testování

* **Pohyb:** `WASD` nebo šipky.
* **Útok:** Klávesa `Space`.
* **Interakce:** Klávesa `E`.
* **UI Menu (Inventář/Mapa/Quest/Settings):** Klávesa `Tab`.

---

* **Autor:** Astrid Hendrychová
* **Projekt:** BCSH1 – Fandango – Jednoduchá počítačová hra
