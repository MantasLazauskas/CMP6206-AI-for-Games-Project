# CMP6206\_Starter-repo

## TODO: Add the following details and update regularly

1. **Name of your project -** Battlefield AI: Soldier Simulation

2. **Description/overview -** A real-time tactical combat simulation where AI soldiers navigate a structured environment, acquire threats via sensory logic, and execute complex tactical decisions. **Core AI Features:**

   1. **Finite State Machines \& Behaviour Trees:** Utilizes FSMs/Behaviour Trees to control unit behaviours, handle fail-safe recovery nodes, and manage tactical transitions (e.g., Patrol -> Engage -> Take Cover). 
   2. **Agent Sensors:** Implemented via raycasts and physics queries to simulate line-of-sight and auditory threat detection. 
   3. **Pathfinding (A\* \& NavMesh):** Integrates Unity's navigation tools and heuristic search logic to navigate around battlefield obstacles efficiently.

3. **Target game engine -** Unity.

4. Research **-** Inspiration: Squad-based tactical shooters and strategic military simulation units. 

**Technical Focus:** Transitioning from rigid FSM architectures to modular, scalable decision-making frameworks (such as Behaviour Trees) to prevent "transition spaghetti" as tactical logic expands. Balancing raycast sensor frequency with performance constraints to handle multiple active agents simultaneously.

5. **Methodology/planning -** Tracked using a Kanban board (GitHub Projects) organized into Backlog, In Progress, Testing, and Done. **Adaptable Roadmap (Weeks 1 – 12+):**

   1. **Weeks 1–2:** Scene setup, environment prototyping, and base FSM framework creation.

   2. **Weeks 3–4:** Line-of-sight sensors, hearing mechanics, and pathfinding integration.
   3. **Week 5**: Combat loop execution, cover evaluation, and state debugging.

   4. **Week 6 (Current):** Integrating Behaviour Trees to improve decision modularity and action recovery.

   5. **Weeks 7–12+ (Upcoming / Flexible):** As new AI topics are covered in sessions—such as Goal-Oriented Action Planning (GOAP), Steering Behaviours \& Boids, Adversarial Search, Reinforcement Learning, Neural Networks, and Genetic Algorithms—I will evaluate each concept and adapt the plan, selectively implementing only the features that I deem necessary and beneficial for this specific simulation.   
   6. **Final Phase:** Quality assurance, system balancing, dynamic UI state debugging, and final documentation polish.
6. **Progress documentation -** 

   1. **Week 1 (Date: 23/09/2026) – Setup \& Architecture:**

      1. **Achieved:** Initialized the Unity project repository, added the official .gitignore, and set up the foundational scene environment.
      2. **Challenges/Learnings:** Ensured version control tracking clean builds without extraneous local library files.
   2. **Weeks 2–3 (Dates: 30/09/2026 - 07/10/2026) – Core FSM \& Sensors:**

      1. **Achieved:** Implemented the base Finite State Machine framework and programmed raycast/overlap sphere sensors for line-of-sight threat acquisition.
      2. **Challenges/Learnings:** Fine-tuned sensor raycast frequencies to prevent performance overhead during active agent updates.
   3. **Weeks 4–5 (Dates: 14/10/2026 - 21/10/2026) – Pathfinding \& Combat Loop:**

      1. **Achieved:** Integrated Unity's NavMesh with heuristic search logic ($A^\*$) and completed the core combat loop (Patrol -> Engage -> Cover).
      2. **Challenges/Learnings:** Addressed state-oscillation bugs where units rapidly flickered between defensive cover states and offensive actions.
   4. **Week 6 (Current - Date: 28/10/2026) – Behaviour Trees Integration:**

      1. **Achieved:** Explored and began integrating Behaviour Trees to handle hierarchical action execution and fail-safe recovery nodes.   
      2. **Challenges/Learnings:** Managed the design trade-off between the modular organization of behaviour trees and the immediate reflex response required in high-intensity combat.

7. **References to sources used -** 

   1. **Engine \& Documentation:** Unity Documentation.   
   2. **Academic \& Textbooks:**

      1. CMP6206 AI for Games Module Lectures (Birmingham City University).  
   3. **Graphics \& Rigs:** Character models and animations sourced from Mixamo.
   4. **Version Control:** Official GitHub Unity .gitignore.

