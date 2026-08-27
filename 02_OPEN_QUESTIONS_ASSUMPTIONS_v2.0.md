# Project-Focused Pomodoro — Open Questions and Assumption Register

**PRD source:** v2.0  
**Date:** 2026-08-26  
**Status:** No blocking questions.

## Open Questions
### OQ-001 — NON-BLOCKING
**Question:** What is the final product name?  
**Impact:** Branding only; does not block implementation of product behavior.  
**Owner:** Product  
**Related requirements:** None

### OQ-002 — NON-BLOCKING
**Question:** What installer/distribution format should be used for the first release?  
**Impact:** Affects packaging/release pipeline, not core application behavior.  
**Owner:** Product/Engineering  
**Related requirements:** NFR-001

### OQ-003 — NON-BLOCKING
**Question:** What representative maximum Project/Task/Subtask/Pomodoro-history volume should define performance benchmarks?  
**Impact:** Needed to replace NFR-005 UNSPECIFIED with measurable UI-performance targets.  
**Owner:** Product  
**Related requirements:** NFR-005

### OQ-004 — NON-BLOCKING
**Question:** Should crash/forced-termination session recovery become a future approved feature?  
**Impact:** Would define next-launch reconciliation UX for interrupted active timers.  
**Owner:** Product  
**Related requirements:** FR-009, FR-022

### OQ-005 — NON-BLOCKING
**Question:** Should custom break duration have a product-level maximum?  
**Impact:** Validation boundary is currently implementation-defined except that the value must be positive.  
**Owner:** Product  
**Related requirements:** FR-014

## Assumptions

### ASM-001 — VALIDATED
**Assumption:** Focus time continues according to wall-clock time while Windows is locked or sleeping.  
**Rationale:** Explicitly confirmed during discovery.  
**Risk:** None remaining for product semantics; implementation must preserve it.  
**Validation method:** Automated injected-clock tests plus Windows 11 lock/sleep manual test.  
**Owner:** Product/Engineering

### ASM-002 — VALIDATED
**Assumption:** Only one Focus or Break timer may be active at any time.  
**Rationale:** Explicitly confirmed during discovery.  
**Risk:** None remaining for product semantics.  
**Validation method:** Start-conflict automated/UI tests.  
**Owner:** Product/Engineering

### ASM-003 — OPEN
**Assumption:** A Project with zero actionable leaf work items uses an empty/not-started progress state rather than a misleading calculated percentage.  
**Rationale:** Needed to make the confirmed progress formula defined for an empty denominator.  
**Risk:** Low; exact visual treatment remains design-level.  
**Validation method:** Review empty-state implementation against FR-007.  
**Owner:** Product/Engineering

### ASM-004 — OPEN
**Assumption:** Moving an existing Task between compatible Milestones/Projects, if implemented, must preserve the Task identity and its historical sessions.  
**Rationale:** History should remain attached to the work performed, not to a newly created replacement record.  
**Risk:** Move functionality itself is not explicitly required in MVP; agent must not add it unless needed/approved.  
**Validation method:** Do not implement move behavior unless separately approved; if added, preserve IDs/history.  
**Owner:** Product

### ASM-005 — OPEN
**Assumption:** Normal process crash/forced termination does not have a user-confirmed auto-resume/recovery flow in MVP.  
**Rationale:** Crash recovery was not approved as product behavior.  
**Risk:** An interrupted active session may need conservative handling or be documented as a limitation.  
**Validation method:** Agent must surface any needed decision instead of inventing recovery UX.  
**Owner:** Product
