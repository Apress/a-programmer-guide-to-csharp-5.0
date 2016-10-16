// polynomial evaluator
// Evaluating y = 5.5 + 7 X^1 + 7 X^2 + 7 X^3 + 7 X^4 + 7 X^5 + 7 X^6

class Poly_3: PolynomialBase.IPolynomial
{
public double Evaluate(double value)
{
    return(
        5.5D
        + value * (7D 
        + value * (15D 
        + value * (30D 
        + value * (500D 
        + value * (100D 
        + value * (1D 
	)))))));
}
}
