namespace JsonConditionEvaluator
{
    public class ConditionEvaluator
    {
        public bool EvaluateCondition(string condition, Dictionary<string, object> jsonData)
        {
            var tokens = ParseCondition(condition);
            return EvaluateTokens(tokens, jsonData);
        }

        private List<string> ParseCondition(string condition)
        {
            List<string> tokens = condition.Replace("and", " and ").Replace("or", " or ").Replace(">", " > ")
                .Replace("<", " < ").Replace(">=", " >= ").Replace("<=", " <= ").Replace("=", " = ").Replace("!=", " != ")
                .Split(' ').Where(token => !string.IsNullOrWhiteSpace(token)).ToList();

            return tokens;
        }

        private bool EvaluateTokens(List<string> tokens, Dictionary<string, object> jsonData)
        {
            Stack<bool> resultStack = new Stack<bool>();

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] == "and" || tokens[i] == "or")
                {
                    bool operand1 = resultStack.Pop();
                    bool operand2 = EvaluateSingleCondition(tokens[i + 1], tokens[i + 2], tokens[i + 3], jsonData);
                    resultStack.Push(tokens[i] == "and" ? operand1 && operand2 : operand1 || operand2);
                    i += 3;
                }
                else
                {
                    bool result = EvaluateSingleCondition(tokens[i], tokens[i + 1], tokens[i + 2], jsonData);
                    resultStack.Push(result);
                    i += 2;
                }
            }

            return resultStack.Pop();
        }

        private bool EvaluateSingleCondition(string key, string op, string value, Dictionary<string, object> jsonData)
        {
            object actualValue = jsonData.ContainsKey(key) ? jsonData[key] : null;

            switch (op)
            {
                case "=":
                    return actualValue != null && actualValue.ToString() == value.Replace("\"", "");
                case "!=":
                    return actualValue != null && actualValue.ToString() != value.Replace("\"", "");
                case ">":
                    return actualValue != null && Convert.ToInt32(actualValue) > Convert.ToInt32(value);
                case "<":
                    return actualValue != null && Convert.ToInt32(actualValue) < Convert.ToInt32(value);
                case ">=":
                    return actualValue != null && Convert.ToInt32(actualValue) >= Convert.ToInt32(value);
                case "<=":
                    return actualValue != null && Convert.ToInt32(actualValue) <= Convert.ToInt32(value);
                default:
                    throw new InvalidOperationException("Invalid operator: " + op);
            }
        }
    }
}
