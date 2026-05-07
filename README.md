# Fandango (Unity Project)

Projekt 2D RPG z ptačí perspektivy vytvořený v enginu Unity.

## 📅 Stav projektu (k 15. 04. 2026)
* **Systémy:** Hlavní Menu, Uživatelské Rozhraní, Ukládání, Dialog, Systém úkolů, Kombat, Zvuk.
* **Obsah:** 3 lokace (úrovně).
* **Režim:** User vs PC (jednoduchá AI nepřátel).

---

## 🎮 Ovládání

* **Pohyb:** `WASD` nebo šipky.
* **Útok:** `Space`.
* **Interakce:** `E`.
* **Menu (Profil/Inventář/Mapa/Deník úkolů/Nastavení):** `Tab`.

---

## 🛠️ Technické specifikace

| # | Kategorie | Detail |
| :--- | :--- | :--- |
| 1 | Engine | Unity 6 (6000.3.10f1) |
| 2 | Skriptování | C# (VS Code) |
| 3 | Fyzika | Rigidbody2D |
| 4 | Grafika | 2D Top-Down Sprite Sheet + Tilemap |
| 5 | Perzistence | Lokální ukládání progresu (Save) |

---

## 📍 Lokace a Level Design

Každá úroveň je navržena pomocí systému **Unity Tilemap** s důrazem na funkční navigaci (Layer-based Physics) a atmosféru.

| # | Lokace | Popis a Funkcionalita | Náhled |
| :--- | :--- | :--- | :--- |
| **1** | **Vesnice** | **Účel:** Tutorial & Safe Zone.<br>• Interakce s NPC.<br>• Příjem úkolů (Quest System).<br>• Interakce s objekty. | <img src="https://github.com/user-attachments/assets/d719b557-7a4a-4600-98fe-c028a049ea52" width="250" /> |
| **2** | **Les** | **Účel:** Průzkum & Základní boj.<br>• Implementace nepřátelské AI (Skeletoni). | <img src="https://github.com/user-attachments/assets/28ad0dbe-b33d-4962-a8e5-d50c4289516d" width="250" /> |
| **3** | **Aréna** | **Účel:** Boss Arena.<br>• Uzavřený prostor pro boss fight.<br>• Implementace variace nepřátele (Boss). | <img src="https://github.com/user-attachments/assets/26150c64-33c4-4463-aea5-e332c7b1cf6a" width="250" /> |

### 🛠️ Technické detaily prostředí
*   **Collision System:** Využití `Tilemap Collider 2D` v kombinaci s `Composite Collider 2D` pro snížení zátěže fyzikálního enginu.
*   **Depth Sorting:** Dynamické vykreslování vrstev (`Sorting Layers`), které umožňuje postavě procházet "za" stromy a budovami pro dosažení hloubky ve 2D.

---

## 📚 Dokumentace assetů a zdrojů

Zde jsou zdokumentovány všechny použité externí zdroje.

### 1. Externí Assety

| # | Typ | Název | Zdroj/Link | Poznámka |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Grafika (UI) | Pixel Hearts 16x16 | [itch.io](https://redreeh.itch.io/pixelhearts-16x16) | Externí asset |
| 2 | Grafika (Items) | Free Pixel Art Coins | [itch.io](https://ok-lavender.itch.io/free-pixel-art-coins) | Externí asset |
| 3 | Grafika (Player/NPC/Enemy) | Skeleton Sprite | [itch.io](https://micaellebritto.itch.io/skeleton-sprite) | Externí asset |
| 4 | Zvuk (SFX) | Pixel Game Essentials | [itch.io](https://jdsherbert.itch.io/pixel-essentials-sfx) | Externí asset |
| 5 | Zvuk (SFX) | Minifantasy Dungeon | [itch.io](https://leohpaz.itch.io/minifantasy-dungeon-sfx) | Externí asset |
| 6 | Grafika (Env) | Pixel Art Top-Down Basic | [itch.io](https://cainos.itch.io/pixel-art-top-down-basic) | Externí asset |
| 7 | Grafika (Items) | Pixel Art Potions 16x16 | [itch.io](https://gelidus-canimons.itch.io/potions-pack-16x16) | Externí asset |

### 2. Tutoriály a vzdělávací zdroje

| # | Typ | Téma | Link/Zdroj | Účel |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Web/Doc | Unity Manual | [Unity Docs](https://docs.unity3d.com/) | API reference |
| 2 | YouTube | Action RPG Series | [NightRun Studio](https://www.youtube.com/watch?v=xZe8m2ujoig) | Pohyb a základní systém |
| 3 | YouTube | 2D Melee Combat | [Blakey Games](https://www.youtube.com/watch?v=rwO3TE1G3ag) | Bojový systém a animace útoku |
| 4 | YouTube | Questing System | [Brackeys](https://www.youtube.com/watch?v=e7VEe_qW4oE) | Implementace úkolů |

### 3. Generativní AI služby

| # | Nástroj | Účel | Výsledek/Použití |
| :--- | :--- | :--- | :--- |
| 1 | Gemini | Scripting | Debugging C# skriptů |

---

* **Autor:** Astrid Hendrychová
* **Projekt:** BCSH1 – Jednoduchá počítačová hra
