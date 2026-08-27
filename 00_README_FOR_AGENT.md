# Project-Focused Pomodoro v2.0 — Agent Package

This package is intended to be handed directly to a coding agent.

## Read in this order

1. `01_PROJECT_POMODORO_PRD_v2.0.md` — **AUTHORITATIVE SOURCE OF TRUTH**
2. `02_OPEN_QUESTIONS_ASSUMPTIONS_v2.0.md` — visible non-blocking unknowns/assumptions
3. `03_CODING_AGENT_HANDOFF_v2.0.md` — implementation mission, milestones, constraints, test plan
4. `04_STRUCTURED_SPEC_v2.0.json` — machine-readable spec validated against the supplied JSON schema
5. `05_QUALITY_REVIEW_v2.0.md` — readiness/quality review

## Agent rule

The PRD wins over the handoff if anything differs. Do not add common project-management features merely because they are conventional. Every implementation task must trace to a PRD requirement ID.

## Confirmed environment

- Windows 11
- C# + .NET 10 + WPF
- Local-only MVP
- No login/cloud/network dependency for core operation

## Important product distinction

**Pomodoro completion is not Task completion.**

Pomodoro history measures focus effort. Project progress is calculated from completed actionable leaf work items. Task/Milestone/Project completion remains explicit user confirmation.

## Structured spec validation

Schema: `/mnt/data/13_PRD_OUTPUT_SCHEMA.json`  
Validation result when this package was generated: **PASS**

The schema does not include a Desktop Application category. The JSON therefore uses `Automation Workflow` only as a compatibility value; the authoritative PRD category is Windows Desktop Productivity / Project-Focus Utility.
