namespace QuizCraft.Generators
{
  public class MultiplicationTableQuestionGenerator : MultiplicationQuestionGenerator
  {
    public MultiplicationTableQuestionGenerator(int table) : base(new int[][]
      {
      new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
      new int[] { table }
      })
    {
    }
  }
}
