const pluginCode = [
  `using System;
  namespace FactorialExample
  {
    class Program
    {
      static void Main(string[] args)
      {
        Console.WriteLine("Enter a number");
        int number = Convert.ToInt32(Console.ReadLine());
        long fact = GetFactorial(number);
        Console.WriteLine("{0} factorial is {1}", number, fact);           
        Console.ReadKey();
      }
     }
  }`,
`public static Image FromFile(String filename,
  bool useEmbeddedColorManagement)
{

if (!File.Exists(filename)) 
{
IntSecurity.DemandReadFileIO(filename);
throw new FileNotFoundException(filename);
}

filename = Path.GetFullPath(filename);

IntPtr image = IntPtr.Zero;
int status;

if (useEmbeddedColorManagement) 
{
status = SafeNativeMethods.Gdip.GdipLoadImageFromFileICM(filename, out image);
}
else 
{
status = SafeNativeMethods.Gdip.GdipLoadImageFromFile(filename, out image);
}`,
`public abstract class myBase
{
    //If you derive from this class you must implement this method. notice we have no method body here either
    public abstract void YouMustImplement();

    //If you derive from this class you can change the behavior but are not required to
    public virtual void YouCanOverride()
    { 
    }
}

public class MyBase
{
   //This will not compile because you cannot have an abstract method in a non-abstract class
    public abstract void YouMustImplement();
}`]

export default pluginCode
