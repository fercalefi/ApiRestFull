using System.Collections.Generic;

namespace ApiRestFull.Data.Converter.Contract
{
    // parametros genericos Origem e Destino
    public interface IParser<O, D>
    {
        D Parse(O origin);
        List<D> Parse(List<O> origin);

    }
}
