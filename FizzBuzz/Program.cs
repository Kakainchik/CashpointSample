using static System.Console;

//Nothing special, just a loop

for(int i = 1; i <= 100; i++)
{
    if(i % 3 == 0 && i % 5 == 0) WriteLine("FizzBuzz");
    else if(i % 3 == 0) WriteLine("Fizz");
    else if(i % 5 == 0) WriteLine("Buzz");
    else WriteLine(i);
}