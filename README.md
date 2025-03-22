# JsonConditionEvaluator

JsonConditionEvaluator is a console-based application that evaluates conditions on a JSON object. It allows users to input a condition (using logical and relational operators) and a JSON object to check whether the object satisfies the given condition.

## Features

- Supports multiple logical (`and`, `or`) and relational operators (`=`, `>`, `<`, `>=`, `<=`, `!=`).
- Dynamically parses JSON objects and evaluates conditions without the need to hardcode fields.
- Continuous input/output loop until the user decides to exit.
- Error handling for invalid input formats and JSON structures.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (for compiling and running C# code)
- Newtonsoft.Json (installed as a NuGet package)

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/JsonConditionEvaluator.git
