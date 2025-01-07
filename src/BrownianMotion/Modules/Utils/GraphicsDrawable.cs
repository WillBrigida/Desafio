using System;

namespace BrownianMotion.Modules.Utils;

public class GraphicsDrawable : IDrawable
{
    private double[] prices;

    public GraphicsDrawable(double[] prices)
    {
        this.prices = prices;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
`        if (prices == null || prices.Length == 0) return;

        float padding = 30; // Espaço de padding ao redor do desenho
        float spot = 10; // Tamanho do ponto 
        float X = padding; // Posição inicial no eixo X
        float minPrice = (float)prices.Min(); // Menor valor 
        float maxPrice = (float)prices.Max(); // Maior valor 
        float range = maxPrice - minPrice; // Intervalo entre o menor e o maior valor

        // Ajusta a largura e altura para considerar o padding
        var width = dirtyRect.Width - 2 * padding;
        var height = dirtyRect.Height - 2 * padding;

        // Espaço horizontal ajustado para começar no início e terminar no fim
        float horizontalSpace = width / (prices.Length - 1);

        // Define a cor de preenchimento para os pontos
        canvas.FillColor = Colors.RoyalBlue;
        var path = new PathF();

        // Normaliza o valor inicial e move o caminho para a posição inicial
        float normalizedY = height - ((float)prices[0] - minPrice) / range * height + padding;
        path.MoveTo(X, normalizedY);
        canvas.FillEllipse(X - spot / 2, normalizedY - spot / 2, spot, spot);

        canvas.StrokeColor = Colors.LightGray; // Define a cor da linha
        canvas.StrokeSize = 2f;
        canvas.DrawLine(X, 0, X, dirtyRect.Height); //Linha guia vertical

        // Itera sobre os preços para desenhar o caminho e os pontos
        for (int i = 1; i < prices.Length; i++)
        {
            X += horizontalSpace;
            normalizedY = height - ((float)prices[i] - minPrice) / range * height + padding;
            path.LineTo(X, normalizedY);


            if (prices.Length < 10 || (i % (prices.Length / 10) == 0))
            {
                //Desenhando os pontos
                canvas.FillEllipse(X - spot / 2, normalizedY - spot / 2, spot, spot);

                //Desenhando as guias verticais
                canvas.DrawLine(X, 0, X, dirtyRect.Height);


                //  canvas.FontColor = Colors.Black;
                // canvas.FontSize = 20;
                // canvas.DrawString($"dia {i}", X, dirtyRect.Height, HorizontalAlignment.Center);
            }
        }

        //Desenhando as guias horizontais
        canvas.DrawLine(0, normalizedY, width, normalizedY); //Linha guia horizontal
        canvas.DrawLine(0, dirtyRect.Height / 2, dirtyRect.Width, dirtyRect.Height / 2); 
        canvas.DrawLine(0, height - ((float)prices.Min() - minPrice) / range * height + padding, dirtyRect.Width, height - ((float)prices.Min() - minPrice) / range * height + padding); 
        canvas.DrawLine(0, height - ((float)prices.Max() - minPrice) / range * height + padding, dirtyRect.Width, height - ((float)prices.Max() - minPrice) / range * height + padding); 

        // Desenhando o caminho no canvas
        canvas.StrokeColor = Colors.RoyalBlue; // Define a cor da linha
        canvas.StrokeSize = 3f; // Define a espessura da linha
        canvas.StrokeLineCap = LineCap.Round; // Define o estilo das extremidades da linha
        canvas.StrokeLineJoin = LineJoin.Round; // Define o estilo das junções da linha
        canvas.DrawPath(path); // Desenha o caminho no canvas
    }

}