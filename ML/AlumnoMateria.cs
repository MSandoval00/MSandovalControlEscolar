﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class AlumnoMateria
    {
        public ML.Alumno Alumno { get; set; }
        public ML.Materia Materia { get; set; }
        public List<object> AlumnosMaterias { get; set; }
    }
}
