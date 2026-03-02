BossRaid 프로젝트입니다.
Unity 2022 사용했습니다.

# 🎮 BossRaid

> 2.5D Boss-Raid Action Prototype  
> Structured with SOLID & Event-Driven Architecture  
> Built in Unity 2022.3 LTS

---

## 📌 Project Vision
Single-boss focused action game where  
all gameplay rules are composed by a single Mediator.

## 🏗 Architecture

Player (Intent Only)
        ↓
Event Layer (Record / Relay)
        ↓
BattleModifierComposeMediator (Single Decision Maker)
        ↓
Executor (Execute Only)

## ⚖ Design Principles

- No type branching (no if bossId / talentId)
- Single decision authority
- Execution layer has no rule logic
- Composition order fixed:
  Base → Promotion → Talent → Difficulty → Clamp

  ## 🚧 Development Status

- [x] Step 1 – Player Intent
- [x] Step 2 – Event Layer
- [x] Step 3 – Mediator Rule Authority
- [x] Step 4 – Actor Execution Model
- [x] Step 5 – Weapon System
- [x] Step 6 – Promotion System
- [x] Step 7 – Core Loop
- [ ] Step 7.5 – Mobile Playable Prototype
- [ ] Step 8 – Boss Pattern Expansion
