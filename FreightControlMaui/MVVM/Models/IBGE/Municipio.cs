﻿namespace FreightControlMaui.MVVM.Models.IBGE
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Microrregiao Microrregiao { get; set; }
    }
}