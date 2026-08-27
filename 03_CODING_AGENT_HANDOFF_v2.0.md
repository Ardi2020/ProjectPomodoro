# Project-Focused Pomodoro — Coding-Agent Handoff

## Handoff Header

| Field | Value |
|---|---|
| Handoff version | 2.0 |
| PRD source | `Project-Focused Pomodoro PRD v2.0` |
| Status | **APPROVED FOR MVP IMPLEMENTATION** |
| Target | Claude Code / OpenAI Codex / GitHub Copilot or equivalent repository-executing coding agent |
| Target OS | Windows 11 |
| Confirmed stack | C# + .NET 10 + WPF |
| Repository status | **Unknown — inspect before proposing exact file paths, package choices, commands, or storage library** |
| Blocking questions | None |

## 1. Implementation Mission

Build the MVP described by PRD v2.0: a local Windows 11 project-focus application where users organize Project → optional Milestone → Task → optional Subtask, start 25-minute Pomodoro sessions from Task/Subtask, use a compact Always-on-Top motivational focus window, preserve immutable effort history, take manual breaks, calculate leaf-based project progress, and explicitly mark work complete.

Do **not** turn this into a general project-management platform.

## 2. Source of Truth and Precedence

1. Latest explicitly approved product decision.
2. `01_PROJECT_POMODORO_PRD_v2.0.md`.
3. This handoff.
4. Repository conventions discovered during inspection.
5. Implementation recommendations.

If this handoff appears to contradict the PRD, **the PRD wins**. Stop and report the conflict rather than silently choosing a different behavior.

## 3. Confirmed Constraints

- Windows 11 desktop.
- C# + .NET 10 + WPF.
- Local-only core MVP.
- No authentication.
- No cloud sync.
- No required network access.
- Focus duration fixed at 25 minutes.
- Break manual: 5 / 10 / custom.
- One active Focus or Break timer.
- Pomodoro only attaches to Task or Subtask.
- Pomodoro completion never auto-completes work.
- Task/Milestone/Project completion is explicit.
- Project progress uses actionable leaf work items.
- Early Focus stop/close requires motivational confirmation.
- Break and completed Focus can stop/close without early-stop friction.
- Work with history cannot be hard-deleted through standard UI.
- Timer truth comes from absolute start/end time, not tick accumulation.

## 4. Mandatory Repository Inspection

Before editing code:
1. Determine whether repository is new or existing.
2. Inspect solution/project files and target framework.
3. Identify current package manager/dependencies.
4. Identify existing persistence, configuration, logging, and test conventions.
5. Identify existing MVVM/application architecture if present.
6. Run existing build/tests if commands can be safely inferred from repository metadata.
7. Report compatibility issues before replacing a confirmed user choice.

**Do not invent repository paths, commands, or libraries before inspection.**

## 5. Architecture Direction

### Required boundaries
Keep these concerns conceptually separate:
- **Domain/project model:** Projects, Milestones, Tasks, Subtasks, progress rules, status transitions.
- **Timer domain service:** absolute start/end boundary calculations; Focus/Break state; single-active-timer invariant.
- **Persistence boundary:** local durable storage and atomic multi-record updates where required.
- **Application/use-case layer:** start focus, stop focus, complete work, take break, archive/restore, delete protection.
- **WPF presentation:** Projects, Project Detail, Work Item Detail, Focus/Break window, dialogs/setup.

Exact folder names/patterns are implementation choices after repository inspection.

### Timer rule
A WPF timer/event may trigger display refresh, but must **never** be the authoritative elapsed-time counter.

Conceptual calculation:
`remaining = max(0, expected_end_at - current_time)`

Reconcile on app/window activation and after Windows lock/sleep.

### Progress rule
```
leaf_units =
  each Task with zero Subtasks
  + each Subtask

progress = DONE leaf_units / all leaf_units
```
Do not include parent Tasks with Subtasks, Milestones, Projects, Pomodoro count, or focused minutes.

## 6. Milestones

### M1 — Repository Baseline and Local Domain Foundation
**Requirements:** NFR-001, NFR-006, FR-022  
**Objective:** Establish buildable/testable Windows 11 .NET 10 WPF baseline consistent with existing repo.  
**Deliverables:**
- Repository inspection report.
- Confirmed build/test commands.
- Domain model boundaries.
- Local persistence boundary/interface.
- No cloud/auth/network dependency.
**Tests:** baseline build/tests; TEST-011 foundation.  
**Completion gate:** application builds; architecture does not contradict confirmed stack/scope.  
**Excluded:** feature UI polish, cloud, telemetry SDK.

### M2 — Project Hierarchy and Progress
**Requirements:** FR-001–FR-007, FR-017–FR-021  
**Objective:** Implement Project/Milestone/Task/Subtask lifecycle and leaf progress.  
**Deliverables:**
- Project + optional Milestone + Task + optional Subtask model.
- TODO/IN_PROGRESS/DONE behavior.
- Why / Desired Outcome.
- Manual completion.
- Incomplete-child warning + Mark Done Anyway.
- Leaf progress.
- Delete protection.
- Project archive/restore.
**Tests:** TEST-001, TEST-002, TEST-008, TEST-010.  
**Completion gate:** all hierarchy/progress acceptance criteria pass.

### M3 — Timer Engine and Pomodoro History
**Requirements:** FR-008, FR-009, FR-016, FR-024, NFR-002, NFR-004  
**Objective:** Implement correct single-session Focus state and history.  
**Deliverables:**
- 25-minute Focus session linked to Task/Subtask.
- Absolute-time start/expected-end calculations.
- COMPLETED and STOPPED_EARLY finalization.
- Actual focused duration.
- Immutable-ended history in MVP UI.
- Single-active-timer enforcement.
**Tests:** TEST-003, TEST-004, TEST-012.  
**Completion gate:** delayed callback and lock/sleep simulations do not extend Focus.

### M4 — Focus Window, Motivation, and Early-Stop Friction
**Requirements:** FR-010–FR-013, FR-023  
**Objective:** Deliver the compact focus experience.  
**Deliverables:**
- Small draggable Always-on-Top window.
- Countdown + Task/Subtask title.
- Optional Why context.
- Scrolling/rotating Focus Messages.
- Early-stop Stop/Close confirmation with Continue Focus / Stop Anyway.
- Focus-complete decision UI.
- Completion sound setting.
**Tests:** TEST-005, TEST-006, TEST-009.  
**Completion gate:** Stop and window Close cannot silently bypass FR-012 while Focus is incomplete.

### M5 — Break and Same-Work Continuation
**Requirements:** FR-014, FR-015, FR-024  
**Objective:** Complete the focus/break loop.  
**Deliverables:**
- 5/10/custom manual Break.
- BREAK Always-on-Top state.
- Frictionless Break stop.
- Break completion references originating work item.
- Start Another Pomodoro or Back to Project.
- No automatic next Focus.
**Tests:** TEST-007.  
**Completion gate:** break never auto-starts Focus and preserves same-work context.

### M6 — Persistence, Failure Paths, Windows Validation, Release Hardening
**Requirements:** FR-020–FR-024, NFR-001–NFR-007  
**Objective:** Validate durable local behavior and release gates.  
**Deliverables:**
- Restart persistence.
- Protected deletes.
- Archive/restore.
- Offline validation.
- Keyboard/accessibility smoke checks.
- Windows lock/sleep manual validation.
- Known limitations documented.
**Tests:** TEST-008, TEST-009, TEST-011, TEST-012 plus full regression.  
**Completion gate:** PRD release criteria all pass or deviations are explicitly reported.

## 7. Module Responsibilities

### Project Domain
**Responsibilities:** hierarchy, ownership, status, manual completion, leaf progress.  
**Inputs:** create/edit/complete/reopen/archive actions.  
**Outputs:** valid domain state/progress.  
**Related:** FR-001–FR-007, FR-017–FR-021.  
**Prohibited coupling:** Timer tick/UI rendering must not be required to calculate project progress.

### Timer Domain
**Responsibilities:** Focus/Break state, absolute timing, one-active-timer invariant.  
**Inputs:** clock, work-item reference, duration, start/stop/complete actions.  
**Outputs:** remaining time, state transition, finalized session.  
**Related:** FR-008, FR-009, FR-013–FR-016, FR-024.  
**Prohibited coupling:** Do not use WPF DispatcherTimer tick count as elapsed-time truth.

### Persistence
**Responsibilities:** durable local hierarchy/settings/session/history; failure-safe writes.  
**Related:** FR-016, FR-020–FR-023, NFR-004.  
**Prohibited coupling:** No remote persistence/network requirement.

### WPF Presentation
**Responsibilities:** Projects, Project Detail, Work Item Detail, Setup, Focus/Break window, confirmation dialogs.  
**Related:** all user-facing FRs.  
**Prohibited coupling:** UI must not silently own domain rules that cannot be independently tested.

## 8. Data Contract

Use PRD DATA-001–DATA-007 as conceptual authority.

Important invariants:
- Task belongs to one Project.
- Milestone, when referenced by Task, belongs to same Project.
- Subtask belongs to one Task.
- PomodoroSession references exactly one Task or Subtask.
- Ended Pomodoro history is not editable through standard MVP UI.
- Own/descendant history blocks standard hard-delete.
- Parent Task DONE does not force children DONE.
- Child completion does not force parent DONE.
- 100% leaf progress does not force Project COMPLETED.

Do not select a concrete local database/storage package solely because it is common. Inspect the repository first; if storage is unspecified, choose the smallest option compatible with integrity/testing needs and report the decision.

## 9. API / Integration Contract

No external API/integration is approved.

If implementation introduces a local repository/service interface, that is an internal architecture contract, not an external product API.

## 10. UI Inventory

### Projects
- Active Project list.
- Progress.
- New Project.
- Access archived Projects.

### Project Detail
- Project title/status/progress.
- Optional Milestones.
- Tasks and nested Subtasks.
- Create/edit actions.
- Completion/archive actions.

### Work Item Detail
- Title.
- Parent context.
- Status.
- optional Why / Desired Outcome.
- Completed Pomodoro count / focused duration summary.
- History.
- Start Pomodoro.
- Mark Done / Reopen.

### Setup
- Focus motivational messages.
- Cancel motivational messages.
- Break 5/10/custom configuration.
- Completion sound On/Off.

### Focus / Break Window
- Always-on-Top.
- Draggable.
- Remaining time.
- Focus: work title, optional Why, motivation.
- BREAK indicator in break state.
- Stop behavior follows PRD.

### Dialogs
- Early Focus stop.
- Delete confirmation/protected delete explanation.
- Incomplete-child Mark Done warning.
- Focus completion next action.

## 11. Security and Privacy Constraints

- No auth system required.
- No secrets required by approved scope.
- No cloud/remote telemetry dependency.
- Treat free-text fields as inert data.
- Validate persisted input and fail safely on corrupted/invalid local data.
- Avoid diagnostics that unnecessarily capture user task/motivation text.
- Do not claim compliance/encryption/security guarantees not evidenced by implementation.

## 12. Test Plan

Use TEST-001 through TEST-012 from the PRD. Minimum mandatory gates:
- unit: progress/status/timer math;
- integration: persistence/history/delete/archive;
- UI/E2E: focus/early-stop/completion/break;
- offline: full core workflow;
- Windows manual: Always-on-Top, drag, sound, lock/sleep;
- failure: local save failures must not report false success.

## 13. Prohibited Assumptions / Scope Additions

Do **not** add without explicit approval:
- adjustable Focus duration;
- auto-break;
- auto-next-Pomodoro;
- automatic Task/Milestone/Project completion;
- automatic next-task selection/prioritization;
- Kanban/Gantt/calendar/dependencies;
- due-date reminder system;
- productivity analytics dashboard/streak/gamification;
- cloud sync/login/accounts;
- team features/comments/assignments;
- AI features;
- web/mobile/browser versions;
- remote telemetry;
- crash-recovery UX beyond conservative reconciliation explicitly required to preserve confirmed timer semantics;
- a new framework replacing WPF/.NET 10.

Do not infer system tray or Windows auto-start behavior.

## 14. Definition of Done

Implementation is done only when:
1. Every MVP FR acceptance criterion is satisfied or a deviation is explicitly approved.
2. Required automated/manual tests pass.
3. No blocking question is hidden.
4. Windows lock/sleep timing is validated.
5. Project progress uses confirmed leaf algorithm.
6. History-protected deletion works.
7. Offline core flow works.
8. No unapproved feature or technology was introduced.
9. Any migration/configuration needed by chosen local persistence is documented.
10. Completion report is produced.

## 15. Mandatory Completion Report Format

At the end, report:

```text
Implementation Summary
- ...

Files Changed
- ...

Requirements Completed
- FR-...
- NFR-...

Tests Run
- command/test:
- result:

Manual Validation
- Windows 11 Always-on-Top:
- Lock/sleep:
- Offline:
- Early-stop:
- Break:

Persistence / Migrations / Configuration
- ...

Deviations from PRD
- NONE
or
- ...

New Assumptions Introduced
- NONE
or
- ...

Known Limitations
- ...

Remaining Work
- ...

Security / Privacy / Operational Concerns
- ...
```

If a requirement cannot be implemented as written, **do not silently change product semantics**. Report the requirement ID, reason, options, and impact.
