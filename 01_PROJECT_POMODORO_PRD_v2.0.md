# Project-Focused Pomodoro — Authoritative Product Requirements Document

## 1. Document Control

| Field | Value |
|---|---|
| Product | Project-Focused Pomodoro *(working name; final branding is OQ-001)* |
| Version | 2.0 |
| Date | 2026-08-26 |
| Status | **IMPLEMENTATION_READY** |
| Interaction mode | Fast discovery |
| Authoritative product category | Windows 11 Desktop Productivity / Project-Focus Utility |
| Structured-schema compatibility category | Automation Workflow *(schema workaround only; not authoritative taxonomy)* |
| Confirmed stack | C# + .NET 10 + WPF |
| Readiness score | 97/100 |
| Quality score | 96/100 |
| Supersedes | Standalone Pomodoro PRD v1.0 for this product direction |
| Blocking questions | None |

### Source-of-truth rule
This PRD is authoritative for MVP behavior. The Coding-Agent Handoff is derived from this document and must not override it. User-confirmed discovery decisions take precedence over recommendations. Unapproved common project-management features are not requirements.

## 2. Executive Summary

A local Windows 11 project-execution application that connects Project/Milestone/Task/Subtask planning to 25-minute Pomodoro focus sessions. Users organize actionable work, start a Pomodoro directly from a Task or Subtask, see work context and motivational messages in a compact Always-on-Top window, deliberately handle early-stop friction, and decide after each completed focus interval whether the work item is done, needs a break, or needs another Pomodoro. Progress is calculated from completed actionable leaf work items, while Task/Milestone/Project completion remains an explicit user decision. The MVP is local-only with no login, cloud sync, collaboration, AI, or network dependency.

The differentiating loop is **Project → actionable Task/Subtask → 25-minute Focus → deliberate completion decision → optional Break → continue or return to Project**. Pomodoro completion measures a focus interval; it never automatically means the underlying Task is done. Project progress measures completed actionable leaf work items, while Task, Milestone, and Project completion remain explicit user decisions.

## 3. Product Vision and Principles

**Vision:** Turn project plans into deliberate, context-rich focus sessions: choose the next actionable work item, focus for 25 minutes, then consciously continue, rest, or mark the work complete.

Principles:
1. **Focus must have context.** Every Focus session belongs to exactly one Task or Subtask.
2. **Time is not completion.** Pomodoro completion and work-item completion are separate concepts.
3. **Progress is calculable; completion is deliberate.** Leaf work drives percentages; humans close work.
4. **Motivation without coercion.** Early-stop friction reminds the user why they started, but Stop Anyway remains available.
5. **Stay small.** The MVP is a local Windows productivity utility, not a collaborative project-management suite.
6. **Preserve history.** Work with focus history is not silently hard-deleted.
7. **Local-first by requirement.** Core functionality requires no account, cloud, or internet.

## 4. Problem Statement

A standalone Pomodoro timer records time without connecting focus to concrete project outcomes. The target user needs a lightweight way to break projects into actionable work, focus on one real Task/Subtask at a time, resist premature stopping, preserve effort history, and explicitly close work when it is actually finished without turning the product into a heavyweight project-management suite.

**Evidence status:** The problem and desired product behavior are user-confirmed through discovery. No external market research or quantified baseline was supplied, so adoption/behavioral impact claims beyond these requirements are not made.

## 5. Goals and Success Metrics

### G-001 — Contextual Focus Execution
**Metric:** Share of started focus sessions linked to exactly one Task or Subtask  
**Target:** 100% of MVP focus sessions  
**Measurement:** Validate PomodoroSession.work_item_id for every started focus session  
**Provenance:** `USER_CONFIRMED`

### G-002 — Trustworthy Work Progress
**Metric:** Project progress calculations that match completed actionable leaf work items  
**Target:** 100% for supported project hierarchies  
**Measurement:** Deterministic unit/integration tests across hierarchy fixtures  
**Provenance:** `USER_CONFIRMED`

### G-003 — Accurate Focus Tracking
**Metric:** Focus session completion and elapsed-time correctness across normal execution and Windows lock/sleep  
**Target:** No session may depend on UI tick count as the source of truth  
**Measurement:** Timer-engine tests using absolute start/end timestamps plus Windows manual validation  
**Provenance:** `USER_CONFIRMED`

### G-004 — Reduce Premature Abandonment
**Metric:** Early focus stop attempts that pass through the motivational confirmation flow  
**Target:** 100% of user-initiated stops before focus completion  
**Measurement:** UI/E2E tests for Stop and window-close paths  
**Provenance:** `USER_CONFIRMED`

### G-005 — Keep the MVP Local and Lightweight
**Metric:** Core product workflows that require login, cloud, or network access  
**Target:** 0  
**Measurement:** Architecture review and offline E2E test  
**Provenance:** `USER_CONFIRMED`


## 6. Users, Roles, and JTBD

### Primary user — Local Windows knowledge worker
**Provenance:** `USER_INFERRED`  
**Context:** Works on personal/professional project work from a Windows 11 PC and wants execution support without collaboration overhead.  
**JTBD:** When I have project work to execute, help me choose a concrete Task/Subtask, stay focused for one Pomodoro, remember why the work matters, and deliberately decide whether it is complete.

**Permissions:** Single local user can create/edit/complete local work, start/stop timers, and manage settings. No authentication or multi-role authorization system is required for MVP.

## 7. Scope

### MVP Included
- Project create/view/edit/manual completion/archive/restore/conditional delete
- Optional Milestones
- Tasks and optional Subtasks with TODO/IN_PROGRESS/DONE
- Optional Why / Desired Outcome on Task/Subtask
- Leaf-work-item progress calculation
- 25-minute Pomodoro started from Task or Subtask
- Multiple Pomodoros per work item
- Small movable Always-on-Top Focus/Break window
- User-configured Focus and Cancel motivational messages
- Early-stop motivational confirmation
- Manual post-focus choice: Mark Done, Take Break, Start Another Pomodoro
- 5/10/custom manual Break
- Return to same work context after Break
- Immutable MVP Pomodoro history and focused-time summary
- Manual Task/Subtask/Milestone/Project completion
- Incomplete-child warning with Mark Done Anyway
- Local persistence, completion sound setting, sleep/lock-correct timing
- Single active Focus or Break timer

### Post-MVP Candidates
- Crash/forced-termination session recovery if later approved
- Performance optimization after representative workload target is known

### Explicitly Out of Scope
- Cloud sync
- Accounts/login
- Multi-device sync
- Teams/collaboration
- Shared projects
- Task assignment
- Comments
- Attachments/file management
- Kanban board
- Gantt chart
- Calendar
- Task dependencies/critical path
- Priority scoring
- Due-date reminder system
- Recurring tasks
- Blocked/On Hold workflow
- Estimate-vs-actual analytics
- Productivity dashboard
- Daily/weekly statistics dashboard
- Streaks/gamification/achievements
- AI task decomposition
- AI recommendations
- Automatic task prioritization
- Automatic next-task selection
- Automatic Pomodoro start
- Automatic Break start
- Web/mobile/browser versions
- Public/collaboration APIs

### Deferred Decisions
- Final product name
- Installer/distribution format
- Exact visual theme/typography/window dimensions
- Exact scrolling speed/message cadence
- System tray behavior
- Windows auto-start behavior
- Representative workload for performance benchmark
- Crash-session recovery UX

## 8. Assumptions and Constraints

### Confirmed constraints
- Windows 11 desktop target.
- C# + .NET 10 + WPF.
- Local-only core functionality; no login/cloud/internet dependency.
- Focus duration is 25 minutes.
- Break is manual and configurable as 5, 10, or custom minutes.
- One active Focus or Break timer maximum.
- Windows lock/sleep does not pause wall-clock passage of the timer.

### Machine-schema limitation
The supplied structured-output schema has no `Desktop Application` category. The JSON export therefore uses `Automation Workflow` strictly as a schema-compatible routing value. The authoritative product type remains a Windows desktop application.

## 9. User Journeys

### J-001 — Create a project and actionable work
**Trigger:** User wants to organize work before focusing.  
**Preconditions:** Application is running.

**Main path**
1. Create a Project.
2. Optionally create one or more Milestones.
3. Create Tasks under the Project or Milestone.
4. Optionally create Subtasks under a Task.
5. Optionally add Why / Desired Outcome to a Task or Subtask.
6. Project detail shows the hierarchy and calculated progress.

**Alternative paths**
- Skip Milestone and create Tasks directly under Project.
- Skip Subtasks and use Task as the actionable leaf item.

**Failure paths**
- Required title is empty and save is rejected.
- Persistence fails and the UI reports failure without creating a partial item.

**Completion state:** At least one actionable Task or Subtask exists and can start a Pomodoro.  
**Requirements:** FR-001, FR-002, FR-003, FR-004, FR-005, FR-007

### J-002 — Start and run a contextual Pomodoro
**Trigger:** User chooses a Task or Subtask and starts focus.  
**Preconditions:** Selected work item is not DONE.; No other Focus or Break timer is active.

**Main path**
1. User selects Start Pomodoro on a Task or Subtask.
2. If the work item is TODO, it becomes IN_PROGRESS.
3. A 25-minute focus session is created and linked to the work item.
4. Small Always-on-Top focus window appears.
5. Window shows remaining time, work-item title, optional Why / Desired Outcome, and scrolling motivation.
6. Remaining time is derived from absolute time boundaries.

**Alternative paths**
- User moves the mini window anywhere on the desktop while it remains Always-on-Top.

**Failure paths**
- A second timer start is rejected while another timer is active.
- Local persistence failure prevents the session from starting and is surfaced to the user.

**Completion state:** Focus session is active and linked to exactly one Task or Subtask.  
**Requirements:** FR-006, FR-008, FR-009, FR-010, FR-011, FR-024

### J-003 — Complete a Pomodoro and choose the next action
**Trigger:** A focus session reaches its 25-minute end time.  
**Preconditions:** A Focus session is active.

**Main path**
1. Session result becomes COMPLETED.
2. Optional completion sound follows the saved setting.
3. Completion UI asks whether the work item is finished.
4. User chooses Mark Task/Subtask Done, Take Break, or Start Another Pomodoro.
5. No work item is auto-completed merely because the focus session completed.

**Alternative paths**
- Mark Done leads to Task completed state with Take Break or Back to Project.
- Take Break leaves the work item IN_PROGRESS.
- Start Another Pomodoro starts another 25-minute session on the same work item.

**Failure paths**
- If persistence of the completed session fails, completion is not silently lost; an error/retry path is shown.

**Completion state:** Completed Pomodoro history is preserved and the user's selected next action is respected.  
**Requirements:** FR-013, FR-014, FR-016, FR-017, FR-023

### J-004 — Attempt to stop focus early
**Trigger:** User presses Stop/Cancel or closes the focus window before the 25-minute focus session is complete.  
**Preconditions:** Focus session is active and remaining time is greater than zero.

**Main path**
1. Application displays a motivational confirmation containing current work context.
2. User may choose Continue Focus or Stop Anyway.
3. Continue Focus preserves the active session.
4. Stop Anyway records STOPPED_EARLY and actual focused duration.
5. The work item remains IN_PROGRESS and is not marked DONE automatically.

**Alternative paths**
- Window-close request uses the same early-stop confirmation.

**Failure paths**
- Closing the confirmation without choosing Stop Anyway leaves the focus session active.

**Completion state:** Either focus continues or an immutable stopped-early history entry exists.  
**Requirements:** FR-012, FR-016

### J-005 — Take a break and return to the same work
**Trigger:** User selects Take Break after focus completion or task completion.  
**Preconditions:** No other timer is active.

**Main path**
1. User starts a 5-minute, 10-minute, or configured custom break.
2. Break timer stays Always-on-Top.
3. Break can be stopped at any time without motivational friction.
4. When break completes, UI identifies the work item from which the break originated.
5. User chooses Start Another Pomodoro or Back to Project.

**Alternative paths**
- User ends the break early and returns to the project without friction.

**Failure paths**
- A second timer start is rejected while Break is active.

**Completion state:** User returns to the same work context or project; focus never auto-starts.  
**Requirements:** FR-014, FR-015, FR-024

### J-006 — Complete work hierarchy manually
**Trigger:** User decides a Subtask, Task, Milestone, or Project is actually complete.  
**Preconditions:** Target item exists.

**Main path**
1. User explicitly invokes Mark Done / Mark Complete.
2. Task/Subtask becomes DONE only by explicit user action.
3. Milestone and Project completion are also explicit.
4. If a parent Task has incomplete Subtasks, a warning appears.
5. User may Cancel or Mark Done Anyway.
6. Progress percentage remains calculated from leaf work-item completion, independent of parent completion state.

**Alternative paths**
- A DONE Task/Subtask can be reopened to IN_PROGRESS.

**Failure paths**
- Persistence failure leaves the prior state intact and reports an error.

**Completion state:** Completion state reflects explicit user confirmation.  
**Requirements:** FR-006, FR-007, FR-017, FR-018, FR-019

### J-007 — Delete, archive, and restore while preserving history
**Trigger:** User wants to remove or hide work.  
**Preconditions:** Target item exists.

**Main path**
1. Items without Pomodoro history may be hard-deleted after confirmation.
2. Items with Pomodoro history cannot be hard-deleted through the standard UI.
3. Projects may be archived without deleting child items or history.
4. Archived projects are excluded from the active list.
5. User may restore an archived project to ACTIVE.

**Alternative paths**
- User cancels destructive confirmation and no data changes.

**Failure paths**
- Persistence failure prevents partial deletion/archive operations.

**Completion state:** History remains referentially intact.  
**Requirements:** FR-020, FR-021, FR-022


## 10. Functional Requirements

### FR-001 — Manage Projects
**Description:** Users can create, view, edit, complete, archive, restore, and conditionally delete Projects. New Projects begin ACTIVE.  
**User value:** Provides the top-level container for focused work.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-001, G-002  
**Related journeys:** J-001, J-006, J-007  
**Dependencies:** None

**Acceptance Criteria**
1. A Project requires a non-empty title after trimming whitespace.
2. A newly created Project has status ACTIVE.
3. Editing a Project preserves all child Milestones, Tasks, Subtasks, and Pomodoro history.
4. Mark Project Complete occurs only after explicit user action; it is never triggered solely by 100% calculated progress.
5. An archived Project is hidden from the active-project list and remains available in an archived-project view.
6. Restoring an archived Project returns it to ACTIVE and preserves children/history.
7. A Project with any descendant Pomodoro history cannot be hard-deleted through the standard UI.

**Relevant states:** Empty | Validation Error | Success | System Error | Destructive Confirmation

### FR-002 — Manage Optional Milestones
**Description:** Users may create, edit, manually complete, and conditionally delete Milestones inside a Project. Milestones are optional.  
**User value:** Supports larger project phases without forcing hierarchy.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-002  
**Related journeys:** J-001, J-006, J-007  
**Dependencies:** None

**Acceptance Criteria**
1. A Project can contain zero Milestones.
2. A Milestone requires a non-empty title.
3. A Task may exist directly under a Project without a Milestone.
4. Completing all work inside a Milestone does not automatically complete the Milestone.
5. When all actionable work under a Milestone is complete, the UI may indicate Ready to complete milestone.
6. A Milestone with descendant Pomodoro history cannot be hard-deleted through the standard UI.

**Relevant states:** Empty | Validation Error | Success | System Error | Destructive Confirmation

### FR-003 — Manage Tasks
**Description:** Users can create, view, edit, manually complete, reopen, and conditionally delete Tasks under a Project or Milestone.  
**User value:** Creates concrete units of project work.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-001, G-002  
**Related journeys:** J-001, J-002, J-006, J-007  
**Dependencies:** None

**Acceptance Criteria**
1. A Task requires a non-empty title.
2. A Task may be created directly under a Project or under a Milestone.
3. A new Task begins TODO.
4. Starting its first Pomodoro changes TODO to IN_PROGRESS.
5. Mark Done changes the Task to DONE only through explicit user action.
6. Reopen changes DONE to IN_PROGRESS.
7. A Task with Pomodoro history cannot be hard-deleted through the standard UI.

**Relevant states:** Empty | Validation Error | Success | System Error | Destructive Confirmation

### FR-004 — Manage Optional Subtasks
**Description:** Users may create, edit, manually complete, reopen, and conditionally delete Subtasks under a Task. Subtasks are optional.  
**User value:** Allows decomposition without making it mandatory.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-001, G-002  
**Related journeys:** J-001, J-002, J-006, J-007  
**Dependencies:** None

**Acceptance Criteria**
1. A Task can contain zero Subtasks.
2. A Subtask requires a non-empty title.
3. A new Subtask begins TODO.
4. A Subtask can start and own its own Pomodoro sessions.
5. The parent Task remains eligible to start Pomodoro sessions even when Subtasks exist.
6. Completing all Subtasks does not automatically mark the parent Task DONE.
7. A Subtask with Pomodoro history cannot be hard-deleted through the standard UI.

**Relevant states:** Empty | Validation Error | Success | System Error | Destructive Confirmation

### FR-005 — Capture Why / Desired Outcome
**Description:** Each Task and Subtask may store an optional Why / Desired Outcome text used as focus context.  
**User value:** Reminds the user why the current work matters.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-001, G-004  
**Related journeys:** J-001, J-002, J-004  
**Dependencies:** None

**Acceptance Criteria**
1. Why / Desired Outcome is optional and does not block saving a Task/Subtask.
2. When present, it is displayed in the work-item detail.
3. When present, it is available to the active focus window and early-stop confirmation.
4. When absent, the UI does not fabricate or download replacement content.

**Relevant states:** Empty | Success | System Error

### FR-006 — Enforce Work-Item Status Transitions
**Description:** Task/Subtask status is TODO, IN_PROGRESS, or DONE; Pomodoro start and explicit user actions control transitions.  
**User value:** Keeps execution state simple and predictable.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-001, G-002  
**Related journeys:** J-002, J-004, J-006  
**Dependencies:** None

**Acceptance Criteria**
1. New Task/Subtask = TODO.
2. Starting the first Pomodoro on a TODO item changes it to IN_PROGRESS.
3. Completing a Pomodoro does not automatically change the work item to DONE.
4. Only explicit Mark Done changes a Task/Subtask to DONE.
5. Reopen changes DONE to IN_PROGRESS.
6. Stopped-early focus leaves the work item IN_PROGRESS.

**Relevant states:** Success | System Error

### FR-007 — Calculate Leaf-Based Project Progress
**Description:** Project progress is the percentage of completed actionable leaf work items; parent Tasks with Subtasks are excluded from the denominator.  
**User value:** Prevents double counting and separates work progress from parent completion confirmation.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-002  
**Related journeys:** J-001, J-006  
**Dependencies:** None

**Acceptance Criteria**
1. A Task without Subtasks counts as one progress unit.
2. A Task with one or more Subtasks does not count as a progress unit; each Subtask counts as one unit.
3. Milestones and Projects never count as progress units.
4. Each progress unit has equal weight in MVP.
5. Progress = DONE leaf units / total leaf units × 100.
6. Pomodoro count or focused minutes never changes progress percentage.
7. A Project with zero leaf units shows an empty/not-started progress state rather than presenting a misleading calculated percentage.

**Relevant states:** Empty | Success | System Error

### FR-008 — Start Pomodoro from Task or Subtask
**Description:** A 25-minute Focus session can be started only from a selected Task or Subtask and is linked to exactly that work item.  
**User value:** Makes every focus session accountable to real project work.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-001, G-003  
**Related journeys:** J-002  
**Dependencies:** None

**Acceptance Criteria**
1. Start Pomodoro is available on eligible Task and Subtask detail.
2. A new focus session stores exactly one work_item_id.
3. The work item may be a parent Task even if it has Subtasks.
4. A DONE work item must be reopened before starting a new Pomodoro.
5. If any Focus or Break timer is already active, a second timer cannot start.
6. Focus planned duration is 25 minutes.

**Relevant states:** Success | Validation Error | System Error | Retry

### FR-009 — Maintain Accurate Focus Time
**Description:** Focus remaining time is derived from absolute start/end timestamps rather than accumulated UI timer ticks.  
**User value:** Ensures lock, sleep, and delayed UI updates do not corrupt focus timing.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-003  
**Related journeys:** J-002, J-003  
**Dependencies:** None

**Acceptance Criteria**
1. At focus start, the system persists a start time and expected end time.
2. Displayed remaining time is recalculated from the expected end time.
3. If Windows lock/sleep lasts beyond the expected end time, the session is treated as completed when the application resumes.
4. UI refresh callbacks are not the authoritative time source.
5. The session cannot report negative remaining time.

**Relevant states:** Success | Stale/Resume | System Error

### FR-010 — Provide Small Movable Always-on-Top Focus Window
**Description:** During Focus and Break, a compact window remains Always-on-Top and can be moved anywhere on the Windows desktop.  
**User value:** Keeps time/context visible without taking over the workspace.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-001, G-004  
**Related journeys:** J-002, J-004, J-005  
**Dependencies:** None

**Acceptance Criteria**
1. Starting Focus opens or activates the compact timer window.
2. The timer window is Always-on-Top while Focus or Break is active.
3. User can drag the window to another desktop location without stopping the timer.
4. Focus window shows remaining time and work-item title.
5. If Why / Desired Outcome exists, it can be shown as contextual text.
6. Closing the window during incomplete Focus routes through FR-012 rather than silently terminating the session.

**Relevant states:** Success | System Error

### FR-011 — Configure and Scroll Motivational Messages
**Description:** Users can manage local Focus motivational messages and early-cancel motivational messages; Focus messages scroll/rotate below the timer.  
**User value:** Maintains the original motivational behavior inside task-context focus.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-004, G-005  
**Related journeys:** J-002, J-004  
**Dependencies:** None

**Acceptance Criteria**
1. Setup provides separate collections for Focus Messages and Cancel Messages.
2. Users can add, edit, and remove their own messages.
3. Saved messages persist across application restarts.
4. During active Focus, available Focus Messages are displayed in a scrolling/rotating area below the timer.
5. When the Focus Message list is empty, focus still works and no unapproved default/network content is generated.

**Relevant states:** Empty | Validation Error | Success | System Error

### FR-012 — Apply Friction to Early Focus Stop
**Description:** Stopping or closing an incomplete Focus session requires a motivational confirmation with Continue Focus and Stop Anyway.  
**User value:** Creates deliberate friction against giving up while preserving user control.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-004  
**Related journeys:** J-004  
**Dependencies:** None

**Acceptance Criteria**
1. Pressing Stop before focus end displays the confirmation.
2. Closing the focus window before focus end displays the same confirmation.
3. Confirmation identifies the active work-item title and may show Why / Desired Outcome and a configured Cancel Message.
4. Continue Focus dismisses the confirmation and preserves the same active session/end time.
5. Stop Anyway ends the session with result STOPPED_EARLY and does not mark the work item DONE.
6. No forced waiting period is required before Stop Anyway.

**Relevant states:** Success | Destructive Confirmation | System Error

### FR-013 — Ask What Happens After Focus Completion
**Description:** When Focus reaches 25 minutes, the user chooses Mark Task/Subtask Done, Take Break, or Start Another Pomodoro.  
**User value:** Separates completion of a focus interval from completion of the underlying work.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-001, G-003  
**Related journeys:** J-003  
**Dependencies:** None

**Acceptance Criteria**
1. At expected end time the session result becomes COMPLETED.
2. The work item is not auto-marked DONE.
3. Completion UI offers Mark Done, Take Break, and Start Another Pomodoro.
4. Start Another Pomodoro creates a new separate 25-minute session on the same work item.
5. After Focus completion the user may close/leave without early-stop friction.

**Relevant states:** Success | System Error | Retry

### FR-014 — Run Configurable Manual Breaks
**Description:** Breaks are started manually, use 5-minute, 10-minute, or configured custom duration, remain Always-on-Top, and can be stopped without friction.  
**User value:** Supports recovery without forcing the user into or out of work.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-003, G-005  
**Related journeys:** J-003, J-005  
**Dependencies:** None

**Acceptance Criteria**
1. Break never starts automatically after Focus.
2. Setup allows 5-minute, 10-minute, or a valid custom break duration.
3. User explicitly starts Break.
4. Break timer remains Always-on-Top.
5. User can stop Break at any time without motivational confirmation.
6. Focus does not auto-start when Break completes.

**Relevant states:** Validation Error | Success | System Error

### FR-015 — Return to the Same Work Context After Break
**Description:** A break retains the originating Task/Subtask context so the user can continue the same work or return to Project.  
**User value:** Reduces context switching between focus intervals.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-001  
**Related journeys:** J-005  
**Dependencies:** None

**Acceptance Criteria**
1. A Break started from a Focus completion stores the originating work-item reference.
2. At Break completion, the UI identifies that originating work item.
3. Actions are Start Another Pomodoro and Back to Project.
4. Start Another Pomodoro targets the same originating work item.
5. No next work item is selected automatically.

**Relevant states:** Success | System Error

### FR-016 — Preserve Pomodoro History
**Description:** Every ended Focus session is stored as immutable MVP history linked to the originating work item and Project.  
**User value:** Provides a trustworthy effort record without using it as completion progress.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-001, G-002, G-003  
**Related journeys:** J-003, J-004  
**Dependencies:** None

**Acceptance Criteria**
1. History stores work item, Project, start time, end time, planned duration, actual focused duration, and result.
2. Supported result values are COMPLETED and STOPPED_EARLY.
3. History records whether the work item was marked DONE as the immediate post-session action when applicable.
4. History is viewable from the work item.
5. History is not editable through the MVP UI.
6. Pomodoro count and focused minutes are summaries of history and do not modify project progress.

**Relevant states:** Empty | Success | System Error

### FR-017 — Complete Tasks/Subtasks Manually with Child Warning
**Description:** Task/Subtask completion is explicit; marking a parent Task DONE while any Subtask is incomplete requires a warning but remains allowed.  
**User value:** Preserves user judgment while preventing accidental closure of unfinished child work.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-002  
**Related journeys:** J-003, J-006  
**Dependencies:** None

**Acceptance Criteria**
1. Mark Done requires an explicit user action.
2. If a Task has no incomplete Subtasks, Mark Done changes it to DONE.
3. If one or more Subtasks are not DONE, the UI reports the number of incomplete Subtasks and offers Cancel and Mark Done Anyway.
4. Cancel preserves the prior Task status.
5. Mark Done Anyway sets the parent Task to DONE without changing child Subtask statuses.
6. Completing a Subtask never auto-completes its parent Task.

**Relevant states:** Success | Destructive Confirmation | System Error

### FR-018 — Complete Milestones Manually
**Description:** Milestones are completed only by explicit user confirmation.  
**User value:** Allows phase review even after all underlying actionable work is done.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-002  
**Related journeys:** J-006  
**Dependencies:** None

**Acceptance Criteria**
1. Completing all leaf work under a Milestone does not change Milestone completion automatically.
2. When all actionable work under a Milestone is DONE, the UI may indicate Ready to complete milestone.
3. Explicit Mark Milestone Complete changes its completion state.
4. Milestone completion does not change Task/Subtask statuses.

**Relevant states:** Empty | Success | System Error

### FR-019 — Complete Projects Manually
**Description:** Projects are completed only by explicit user confirmation, independent of calculated progress.  
**User value:** Ensures project closure reflects user judgment rather than arithmetic alone.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-002  
**Related journeys:** J-006  
**Dependencies:** None

**Acceptance Criteria**
1. A Project reaching 100% leaf progress is not auto-completed.
2. At 100% progress the UI may indicate All project tasks completed / Ready to complete.
3. Explicit Mark Project Complete changes status to COMPLETED.
4. Completing a Project preserves the full hierarchy and Pomodoro history.

**Relevant states:** Empty | Success | System Error

### FR-020 — Protect History During Deletion
**Description:** Hard deletion through the standard UI is allowed only when the target item and its descendants have no Pomodoro history.  
**User value:** Prevents orphaned or misleading focus history.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-002  
**Related journeys:** J-007  
**Dependencies:** None

**Acceptance Criteria**
1. Delete requires confirmation.
2. An item with no own or descendant Pomodoro history may be hard-deleted.
3. An item with own or descendant Pomodoro history cannot be hard-deleted through standard UI.
4. Rejected deletion leaves the hierarchy and history unchanged.
5. Cancelling delete leaves data unchanged.

**Relevant states:** Destructive Confirmation | Permission Denied/Protected | System Error

### FR-021 — Archive and Restore Projects
**Description:** Projects can be archived and restored while preserving all hierarchy and history.  
**User value:** Keeps inactive work out of the active list without destroying records.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-002, G-005  
**Related journeys:** J-007  
**Dependencies:** None

**Acceptance Criteria**
1. Archive removes the Project from the active-project list.
2. Archive preserves Milestones, Tasks, Subtasks, Why text, and Pomodoro history.
3. Archived Project appears in an archived-project view/list.
4. Restore returns the Project to ACTIVE.
5. Restore does not alter child status or history.

**Relevant states:** Empty | Success | System Error

### FR-022 — Persist MVP Data Locally
**Description:** Projects, hierarchy, settings, messages, timer state needed for lock/sleep correctness, and history are stored locally on the Windows device.  
**User value:** Allows the application to work without accounts, cloud, or internet.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-005  
**Related journeys:** J-001, J-002, J-007  
**Dependencies:** None

**Acceptance Criteria**
1. Core workflows function with the device offline.
2. No login is required.
3. Project/work-item data and settings remain after normal application restart.
4. No third-party network service is required for MVP operation.
5. The implementation must not add cloud sync or telemetry as an unstated MVP dependency.

**Relevant states:** Offline | Success | System Error

### FR-023 — Configure Completion Sound
**Description:** Users can enable or disable a local sound when a Focus session completes.  
**User value:** Provides completion feedback without making sound mandatory.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-003  
**Related journeys:** J-003  
**Dependencies:** None

**Acceptance Criteria**
1. Setup exposes completion sound On/Off.
2. The setting persists locally.
3. When On, a Focus completion produces the configured application completion sound behavior.
4. When Off, Focus completion is silent.
5. Stopped-early Focus does not trigger Focus-complete sound.

**Relevant states:** Success | System Error

### FR-024 — Allow Only One Active Timer
**Description:** The application permits at most one active Focus or Break timer at a time.  
**User value:** Prevents ambiguous session ownership and timing.  
**Priority:** MVP  
**Provenance:** `USER_CONFIRMED`  
**Related goals:** G-001, G-003  
**Related journeys:** J-002, J-005  
**Dependencies:** None

**Acceptance Criteria**
1. Starting Focus while Focus or Break is active is rejected.
2. Starting Break while Focus or Break is active is rejected.
3. After the active timer ends or is explicitly stopped, a new timer can start.
4. The UI clearly identifies the currently active timer type and associated work item where applicable.

**Relevant states:** Validation Error | Success


## 11. UX and Interaction Requirements

### Information architecture
The MVP has three primary areas:
1. **Projects** — active project list, progress, create Project, access archived Projects.
2. **Project Detail** — hierarchy of optional Milestones, Tasks, and optional Subtasks with status/progress.
3. **Work Item Detail + Focus Window** — work context, Why / Desired Outcome, history, Start Pomodoro, Mark Done, and compact timer.

### Focus window
- Small, movable, Always-on-Top during Focus/Break.
- Focus shows remaining time, work-item title, optional Why text, scrolling/rotating motivational text.
- Focus Stop/Close before completion invokes early-stop confirmation.
- Completed Focus can be closed without motivational friction.
- Break clearly indicates BREAK and is stoppable without friction.

### Content hierarchy
During Focus, prioritize:
1. remaining time;
2. work-item title (WHAT);
3. optional Why / Desired Outcome (WHY);
4. motivational message (MOTIVATION).

### Destructive/important confirmations
- Delete confirmation.
- Early-stop confirmation.
- Parent Task completion warning when incomplete Subtasks exist.

Exact visual styling, dimensions, typography, color, and animation remain implementation/design details unless separately approved.

## 12. Application States

| State | Required behavior |
|---|---|
| Initial/empty Projects | Explain there are no Projects and provide New Project action. |
| Empty Project / no leaf work | Show no actionable work; do not show a misleading percentage. |
| Active Focus | Show contextual 25-minute timer and motivation. |
| Focus Complete | Preserve completed history; offer Mark Done / Take Break / Another Pomodoro. |
| Early Stop | Confirmation; Continue Focus or Stop Anyway. |
| Active Break | Always-on-Top BREAK timer; stop without friction. |
| Break Complete | Offer same-work Pomodoro or Back to Project; do not auto-start. |
| Validation Error | Keep user-entered content where practical and explain invalid required value. |
| Local Persistence Error | Do not silently claim success; retain previous durable state and expose retry/recovery path where applicable. |
| Protected Delete | Explain that history prevents hard-delete. |
| Incomplete-child completion | Show incomplete count; Cancel or Mark Done Anyway. |
| Windows lock/sleep resume | Recalculate from absolute expected end time. |
| Offline | Core app continues to work. |

Not applicable in MVP: unauthorized/login state, server rate limits, third-party timeout states, multi-tenant permission denial.

## 13. Roles and Permission Matrix

| Role | Capability | Allowed | Conditions |
|---|---|---:|---|
| Local user | View/create/edit Projects/work items/settings | Yes | Local single-user MVP |
| Local user | Start/stop Focus and Break | Yes | Only one active timer |
| Local user | Mark work complete/reopen | Yes | Explicit action |
| Local user | Hard-delete items without history | Yes | Confirmation required |
| Local user | Hard-delete items with own/descendant Pomodoro history | No | Preserve history; Project archive is available |
| Local user | Cloud/team/admin actions | No | Out of scope |

## 14. Data Requirements

### DATA-001 — Project
**Description:** Top-level project container.  
**Fields:** id, title, status: ACTIVE|COMPLETED|ARCHIVED, created_at, updated_at  
**Validation:** title non-empty after trim  
**Sensitivity:** Local productivity data; no regulated classification confirmed  
**Lifecycle:** Create/edit; manual complete; archive/restore; hard delete only if no descendant history  
**Provenance:** `USER_CONFIRMED`

### DATA-002 — Milestone
**Description:** Optional phase within a Project.  
**Fields:** id, project_id, title, completion_state, created_at, updated_at  
**Validation:** title non-empty; project_id required  
**Sensitivity:** Local productivity data  
**Lifecycle:** Create/edit/manual complete; conditional hard delete  
**Provenance:** `USER_CONFIRMED`

### DATA-003 — Task
**Description:** Concrete work item under Project or Milestone.  
**Fields:** id, project_id, milestone_id optional, title, why_desired_outcome optional, status TODO|IN_PROGRESS|DONE, created_at, updated_at  
**Validation:** title non-empty; must belong to Project; milestone when set must belong to same Project  
**Sensitivity:** Local productivity data  
**Lifecycle:** Create/edit/reopen/complete; conditional hard delete  
**Provenance:** `USER_CONFIRMED`

### DATA-004 — Subtask
**Description:** Optional child work item under Task.  
**Fields:** id, task_id, title, why_desired_outcome optional, status TODO|IN_PROGRESS|DONE, created_at, updated_at  
**Validation:** title non-empty; task_id required  
**Sensitivity:** Local productivity data  
**Lifecycle:** Create/edit/reopen/complete; conditional hard delete  
**Provenance:** `USER_CONFIRMED`

### DATA-005 — PomodoroSession
**Description:** Immutable-ended focus history linked to exactly one Task or Subtask.  
**Fields:** id, project_id, work_item_type Task|Subtask, work_item_id, started_at, expected_end_at, ended_at, planned_duration, actual_focus_duration, result COMPLETED|STOPPED_EARLY, marked_done_after_session boolean  
**Validation:** exactly one work item; planned focus duration 25 minutes for MVP; actual duration non-negative  
**Sensitivity:** Local productivity activity data  
**Lifecycle:** Created at focus start; finalized on complete/early stop; ended history not editable in MVP UI  
**Provenance:** `USER_CONFIRMED`

### DATA-006 — BreakSession
**Description:** Temporary/local break timing state retaining originating work-item context when applicable.  
**Fields:** id, originating_work_item_id optional, started_at, expected_end_at, ended_at optional, planned_duration  
**Validation:** duration must be a valid configured break duration  
**Sensitivity:** Local productivity activity data  
**Lifecycle:** Created on explicit Start Break; finalized on completion/early stop  
**Provenance:** `USER_CONFIRMED`

### DATA-007 — Settings
**Description:** Local Pomodoro and UI behavior settings.  
**Fields:** break_duration option/custom, completion_sound_enabled, focus_messages, cancel_messages, timer_window_position optional  
**Validation:** custom break duration must be positive; exact maximum UNSPECIFIED  
**Sensitivity:** Local user preferences and free-text motivation  
**Lifecycle:** Persist until user edits/removes or app data is deleted  
**Provenance:** `USER_CONFIRMED`


### Relationship rules
- Project has zero or more Milestones and Tasks.
- Milestone belongs to exactly one Project.
- Task belongs to exactly one Project and optionally one Milestone within that same Project.
- Subtask belongs to exactly one Task.
- PomodoroSession belongs to exactly one actionable work item (Task or Subtask) and records Project context.
- Break may retain originating work-item context.
- Ended Pomodoro history is immutable through MVP UI.

### Progress formula
`completed_leaf_count / total_leaf_count * 100`

Leaf definition:
- Task without Subtasks = leaf.
- Task with Subtasks = not a progress unit.
- Each Subtask = leaf.
- Project and Milestone = never a progress unit.
- All leaf units have equal MVP weight.

## 15. Integration Requirements

No external integrations are required or approved for MVP.

## 16. AI Requirements

Not applicable. AI features are explicitly out of scope.

## 17. Non-Functional Requirements

### NFR-001 — Compatibility
**Requirement:** The MVP shall run as a Windows 11 desktop application using the confirmed C# + .NET 10 + WPF stack.  
**Target:** Windows 11; C#; .NET 10; WPF  
**Measurement:** Build and manual smoke test on supported Windows 11 environment  
**Scope:** Entire MVP  
**Provenance:** `USER_CONFIRMED`

### NFR-002 — Reliability
**Requirement:** Timer correctness shall be based on absolute time boundaries so delayed UI callbacks, lock, or sleep do not extend the planned Focus interval.  
**Target:** No authoritative timing from tick accumulation  
**Measurement:** Unit tests with injected clock plus lock/sleep manual validation  
**Scope:** Focus and Break timer engine  
**Provenance:** `USER_CONFIRMED`

### NFR-003 — Privacy
**Requirement:** Core MVP operation shall not require transmitting project, task, motivational-message, or Pomodoro-history data to a remote service.  
**Target:** 0 required remote data transfers for core MVP  
**Measurement:** Architecture/network inspection during release validation  
**Scope:** Core MVP  
**Provenance:** `USER_CONFIRMED`

### NFR-004 — Reliability
**Requirement:** Local write operations shall avoid intentionally leaving partial hierarchy/session records when a save operation fails.  
**Target:** Atomic/transactional behavior where multiple persisted records must change together; exact storage mechanism is implementation-defined  
**Measurement:** Failure-path integration tests  
**Scope:** Local persistence  
**Provenance:** `GPT_RECOMMENDED`

### NFR-005 — Performance
**Requirement:** Project-detail and history UI performance shall have an explicit benchmark before optimization claims are made.  
**Target:** UNSPECIFIED pending expected project/work-item/history volume  
**Measurement:** Define benchmark fixtures before performance acceptance testing  
**Scope:** Project and history views  
**Provenance:** `UNSPECIFIED`

### NFR-006 — Maintainability
**Requirement:** Timer calculation, project hierarchy/progress calculation, persistence, and WPF presentation logic should be separable enough to unit-test domain behavior without driving the UI.  
**Target:** Domain timer/progress rules covered by automated unit tests  
**Measurement:** Test suite and code review  
**Scope:** Application architecture  
**Provenance:** `GPT_RECOMMENDED`

### NFR-007 — Accessibility
**Requirement:** Primary controls shall be usable with keyboard navigation and shall expose meaningful accessible names where supported by WPF.  
**Target:** Keyboard path for create/edit/start/stop/complete/break flows; exact WCAG conformance level UNSPECIFIED  
**Measurement:** Manual keyboard/accessibility smoke test  
**Scope:** Primary MVP windows/dialogs  
**Provenance:** `GPT_RECOMMENDED`


## 18. Security and Privacy

- No authentication is required for the confirmed single-local-user MVP.
- No secrets or remote-service credentials are required by product scope.
- Core MVP must not require remote transfer of Project, Task/Subtask, motivational-message, or Pomodoro-history data.
- Free-text titles, Why text, and motivational messages are data only; they must not be interpreted/executed as commands.
- Avoid logging unnecessary user free-text in diagnostics.
- Do not add cloud sync, remote telemetry, or third-party SDKs as hidden dependencies.
- This PRD does not claim compliance, encryption, or security certification.

## 19. Analytics and Observability

### Product data
Pomodoro history is local product data:
- completed/stopped-early result;
- timestamps;
- actual focused duration;
- linked work item;
- whether the immediate post-session action marked the work item Done.

### Remote analytics
None approved for MVP.

### Operational diagnostics
Implementation may provide local diagnostics sufficient to investigate persistence/timer failures, but should not unnecessarily record motivational or Why text.

## 20. Testing and Validation

### TEST-001 — Unit
**Intent:** Validate leaf-work-item project progress including parent Tasks with Subtasks and zero-leaf projects.  
**Requirements:** FR-007

### TEST-002 — Unit
**Intent:** Validate Task/Subtask status transitions and no automatic DONE on Pomodoro completion.  
**Requirements:** FR-006, FR-013, FR-017

### TEST-003 — Unit
**Intent:** Validate absolute-time timer behavior with normal passage, delayed callbacks, lock/sleep simulation, and overdue resume.  
**Requirements:** FR-009, FR-024

### TEST-004 — Integration
**Intent:** Validate Pomodoro start/finalization persistence and immutable completed/stopped-early history.  
**Requirements:** FR-008, FR-016, FR-022

### TEST-005 — UI/E2E
**Intent:** Validate Always-on-Top focus window, draggable behavior, context display, and early-stop confirmation via Stop and Close.  
**Requirements:** FR-010, FR-012

### TEST-006 — UI/E2E
**Intent:** Validate completion choices: Mark Done, Take Break, and Start Another Pomodoro without auto-completion.  
**Requirements:** FR-013, FR-017, FR-023

### TEST-007 — UI/E2E
**Intent:** Validate 5/10/custom manual break, frictionless break stop, same-work return, and no auto-started focus.  
**Requirements:** FR-014, FR-015, FR-024

### TEST-008 — Integration
**Intent:** Validate delete protection when own/descendant history exists and archive/restore preservation.  
**Requirements:** FR-020, FR-021, FR-022

### TEST-009 — Integration
**Intent:** Validate local persistence of Projects, hierarchy, Why text, motivation messages, settings, and history across restart.  
**Requirements:** FR-001, FR-002, FR-003, FR-004, FR-005, FR-011, FR-022, FR-023

### TEST-010 — UI/E2E
**Intent:** Validate incomplete-subtask warning and Mark Done Anyway without altering child statuses.  
**Requirements:** FR-017

### TEST-011 — Offline E2E
**Intent:** Validate core project/focus/break/history workflows without network access or login.  
**Requirements:** FR-022

### TEST-012 — Manual Windows
**Intent:** Validate expected Focus completion after Windows 11 lock/sleep exceeds remaining focus time.  
**Requirements:** FR-009, NFR-001, NFR-002


### Mandatory manual validation
- Windows 11 Always-on-Top behavior.
- Drag/move focus window.
- Stop and window-close early-focus paths.
- Focus-completion sound On/Off.
- Break stop without friction.
- Lock/sleep reconciliation.
- Fully offline core workflow.

## 21. Release Criteria

### Definition of Ready
- This PRD v2.0 is the source of truth.
- No blocking open questions.
- Repository inspected before exact paths/libraries are assumed.
- Confirmed stack retained.

### Definition of Done
- All MVP FR acceptance criteria pass.
- All blocking questions remain resolved; non-blocking questions are documented.
- Automated progress/status/timer tests pass.
- Windows 11 Always-on-Top, drag, early-stop, sound, break, and lock/sleep manual tests pass.
- Offline core-flow test passes without login/network.
- Delete protection and archive/restore preserve history.
- Coding agent reports any deviation from this PRD; no unapproved feature/technology is introduced.

### Launch blockers
- Any MVP FR without passing acceptance criteria.
- Progress algorithm double-counts parent Task + Subtasks.
- Timer uses tick accumulation as authoritative elapsed time.
- Early Focus Stop/Close bypasses confirmation.
- Completed Pomodoro auto-completes a Task without explicit action.
- Delete can remove work with history.
- Core app requires login/network/cloud.
- Coding-agent handoff introduces unapproved features or technologies.

## 22. Risks and Mitigations

### RISK-001
**Risk:** Progress can become misleading if parent Tasks and Subtasks are double-counted.  
**Likelihood:** MEDIUM  
**Impact:** HIGH  
**Mitigation:** Use the confirmed leaf-work-item algorithm and automated fixtures.  
**Contingency:** Disable/revert progress display if reconciliation tests fail.  
**Related:** FR-007  
**Status:** MITIGATED_BY_DESIGN

### RISK-002
**Risk:** UI timer tick accumulation can make sessions inaccurate across sleep/lock or delayed rendering.  
**Likelihood:** HIGH  
**Impact:** HIGH  
**Mitigation:** Use persisted absolute start/expected-end timestamps; UI callbacks only refresh display.  
**Contingency:** Reconcile active session against current clock on resume/activation.  
**Related:** FR-009  
**Status:** MITIGATED_BY_DESIGN

### RISK-003
**Risk:** Deleting work items with focus history can orphan or misrepresent historical effort.  
**Likelihood:** MEDIUM  
**Impact:** HIGH  
**Mitigation:** Block standard hard-delete when own/descendant Pomodoro history exists; archive Projects instead.  
**Contingency:** Preserve records and expose a non-destructive archive path.  
**Related:** FR-020, FR-021  
**Status:** MITIGATED_BY_DESIGN

### RISK-004
**Risk:** A parent Task can be marked DONE while child Subtasks remain incomplete, creating intentionally inconsistent-looking states.  
**Likelihood:** MEDIUM  
**Impact:** MEDIUM  
**Mitigation:** Show count-aware warning and require Mark Done Anyway confirmation; do not silently mutate child status.  
**Contingency:** Allow reopening parent Task.  
**Related:** FR-017  
**Status:** ACCEPTED_WITH_WARNING

### RISK-005
**Risk:** Unknown expected project/history volume means UI performance targets cannot yet be objectively certified.  
**Likelihood:** UNKNOWN  
**Impact:** MEDIUM  
**Mitigation:** Keep target UNSPECIFIED and add benchmark fixtures before performance claims.  
**Contingency:** Profile and optimize after representative volume is known.  
**Related:** NFR-005  
**Status:** OPEN_NON_BLOCKING

### RISK-006
**Risk:** Unexpected process termination can leave an active session requiring reconciliation on next launch.  
**Likelihood:** MEDIUM  
**Impact:** MEDIUM  
**Mitigation:** Persist enough active timer boundary state for lock/sleep correctness; crash-recovery UX is explicitly not an approved MVP feature.  
**Contingency:** Report unresolved active session behavior as a known limitation rather than inventing recovery semantics.  
**Related:** FR-009, FR-022  
**Status:** OPEN_NON_BLOCKING


## 23. Open Questions

- **OQ-001 — Non-blocking:** What is the final product name? Impact: Branding only; does not block implementation of product behavior.
- **OQ-002 — Non-blocking:** What installer/distribution format should be used for the first release? Impact: Affects packaging/release pipeline, not core application behavior.
- **OQ-003 — Non-blocking:** What representative maximum Project/Task/Subtask/Pomodoro-history volume should define performance benchmarks? Impact: Needed to replace NFR-005 UNSPECIFIED with measurable UI-performance targets.
- **OQ-004 — Non-blocking:** Should crash/forced-termination session recovery become a future approved feature? Impact: Would define next-launch reconciliation UX for interrupted active timers.
- **OQ-005 — Non-blocking:** Should custom break duration have a product-level maximum? Impact: Validation boundary is currently implementation-defined except that the value must be positive.

## 24. Assumption Register

- **ASM-001 — VALIDATED:** Focus time continues according to wall-clock time while Windows is locked or sleeping.  
  **Rationale:** Explicitly confirmed during discovery.  
  **Risk:** None remaining for product semantics; implementation must preserve it.  
  **Validation:** Automated injected-clock tests plus Windows 11 lock/sleep manual test.
- **ASM-002 — VALIDATED:** Only one Focus or Break timer may be active at any time.  
  **Rationale:** Explicitly confirmed during discovery.  
  **Risk:** None remaining for product semantics.  
  **Validation:** Start-conflict automated/UI tests.
- **ASM-003 — OPEN:** A Project with zero actionable leaf work items uses an empty/not-started progress state rather than a misleading calculated percentage.  
  **Rationale:** Needed to make the confirmed progress formula defined for an empty denominator.  
  **Risk:** Low; exact visual treatment remains design-level.  
  **Validation:** Review empty-state implementation against FR-007.
- **ASM-004 — OPEN:** Moving an existing Task between compatible Milestones/Projects, if implemented, must preserve the Task identity and its historical sessions.  
  **Rationale:** History should remain attached to the work performed, not to a newly created replacement record.  
  **Risk:** Move functionality itself is not explicitly required in MVP; agent must not add it unless needed/approved.  
  **Validation:** Do not implement move behavior unless separately approved; if added, preserve IDs/history.
- **ASM-005 — OPEN:** Normal process crash/forced termination does not have a user-confirmed auto-resume/recovery flow in MVP.  
  **Rationale:** Crash recovery was not approved as product behavior.  
  **Risk:** An interrupted active session may need conservative handling or be documented as a limitation.  
  **Validation:** Agent must surface any needed decision instead of inventing recovery UX.

## 25. Requirement Traceability Matrix

| Requirement | Goal(s) | Journey(s) | Acceptance criteria | Tests | Status |
|---|---|---|---|---|---|
| FR-001 | G-001, G-002 | J-001, J-006, J-007 | Yes | TEST-009 | MVP |
| FR-002 | G-002 | J-001, J-006, J-007 | Yes | TEST-009 | MVP |
| FR-003 | G-001, G-002 | J-001, J-002, J-006, J-007 | Yes | TEST-009 | MVP |
| FR-004 | G-001, G-002 | J-001, J-002, J-006, J-007 | Yes | TEST-009 | MVP |
| FR-005 | G-001, G-004 | J-001, J-002, J-004 | Yes | TEST-009 | MVP |
| FR-006 | G-001, G-002 | J-002, J-004, J-006 | Yes | TEST-002 | MVP |
| FR-007 | G-002 | J-001, J-006 | Yes | TEST-001 | MVP |
| FR-008 | G-001, G-003 | J-002 | Yes | TEST-004 | MVP |
| FR-009 | G-003 | J-002, J-003 | Yes | TEST-003, TEST-012 | MVP |
| FR-010 | G-001, G-004 | J-002, J-004, J-005 | Yes | TEST-005 | MVP |
| FR-011 | G-004, G-005 | J-002, J-004 | Yes | TEST-009 | MVP |
| FR-012 | G-004 | J-004 | Yes | TEST-005 | MVP |
| FR-013 | G-001, G-003 | J-003 | Yes | TEST-002, TEST-006 | MVP |
| FR-014 | G-003, G-005 | J-003, J-005 | Yes | TEST-007 | MVP |
| FR-015 | G-001 | J-005 | Yes | TEST-007 | MVP |
| FR-016 | G-001, G-002, G-003 | J-003, J-004 | Yes | TEST-004 | MVP |
| FR-017 | G-002 | J-003, J-006 | Yes | TEST-002, TEST-006, TEST-010 | MVP |
| FR-018 | G-002 | J-006 | Yes | TBD | MVP |
| FR-019 | G-002 | J-006 | Yes | TBD | MVP |
| FR-020 | G-002 | J-007 | Yes | TEST-008 | MVP |
| FR-021 | G-002, G-005 | J-007 | Yes | TEST-008 | MVP |
| FR-022 | G-005 | J-001, J-002, J-007 | Yes | TEST-004, TEST-008, TEST-009, TEST-011 | MVP |
| FR-023 | G-003 | J-003 | Yes | TEST-006, TEST-009 | MVP |
| FR-024 | G-001, G-003 | J-002, J-005 | Yes | TEST-003, TEST-007 | MVP |
| NFR-001 | G-003 | — | Yes | TEST-012 | MVP |
| NFR-002 | G-003 | — | Yes | TEST-012 | MVP |
| NFR-003 | G-005 | — | Yes | TBD | MVP |
| NFR-004 | — | — | Yes | TBD | MVP |
| NFR-005 | — | — | Yes | TBD | MVP_TARGET_UNSPECIFIED |
| NFR-006 | — | — | Yes | TBD | MVP |
| NFR-007 | — | — | Yes | TBD | MVP |

## Quality Gate Summary

- Completeness: strong; all Level-C sections applicable to this product are present.
- Requirement clarity: all MVP behaviors carry stable IDs and explicit boundaries.
- Testability: every MVP FR has objective acceptance criteria; negative/destructive paths are covered.
- Traceability: FR/NFR → goals/journeys → tests is explicit.
- Scope discipline: project-management, cloud, AI, collaboration, analytics-suite features are explicitly excluded.
- UX/state coverage: empty, active, completion, validation, persistence error, early-stop, delete protection, child-warning, break, offline, and lock/sleep states are covered.
- Technical readiness: confirmed Windows/.NET/WPF stack; exact persistence library/repository structure remains intentionally unselected until repository inspection.
- Security/privacy: local-only boundaries are explicit; no unsupported compliance/security claims.
- Risk transparency: performance volume and crash-recovery UX remain visible non-blocking unknowns.
- Coding-agent readiness: separate handoff provided with milestone ordering, prohibited assumptions, test mapping, and completion-report format.

**Quality score:** 96/100. This indicates strong coding-agent handoff readiness, not a guarantee of defect-free implementation.
