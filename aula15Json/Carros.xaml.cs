using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Newtonsoft.Json.Linq;

namespace aula15Json
{
    public partial class Carros : PhoneApplicationPage
    {
        public Carros()
        {
            InitializeComponent();


        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            lstCarrosLuxo.ItemsSource = GetCarros("luxo");
        }

        public List<clsCarros> GetCarros(String tipo)
        {
            String JSON = readFile("Resources/json/carros_" + tipo + ".json");
            List<clsCarros> carros = ParserJSON(JSON);
            return carros;
          
        }


        /// <summary>
        /// Método que faz um parser num arquivo JSON
        /// </summary>
        /// <param name="pJSON">passa o arquivo no formato JSON como string</param>
        /// <returns>retorna uma lista genérica de objetos do tipo clsCarros</returns>
        private List<clsCarros> ParserJSON(string pJSON)
        {
            //criar um objeto lista do tipo clsCarros
            List<clsCarros> lista = new List<clsCarros>();

            //Se o JSON está presente
            if (pJSON != null)
            {
                //faz o parser para um tipo JObject
                JObject jobject = JObject.Parse(pJSON);
                
                //Le o objeto da lista carros
                JObject jobjectCarros = (JObject)jobject["carros"];

                //captura o array carro do objeto list Carros
                JArray carros = (JArray)jobjectCarros["carro"];
                
                //percorre o array e para cada JObject carro adiciona na lista genérica
                foreach (JObject carro in carros)
                {
                    clsCarros c = new clsCarros()
                    {
                        Nome = (String)carro["nome"],
                        Descricao = (String)carro["desc"],
                        Imagem = (String)carro["url_foto"]
                       
                    };

                    lista.Add(c);
                }
            }
            return lista;
        }

        /// <summary>
        /// Método para ler um arquivo de dentro do projeto
        /// </summary>
        /// <param name="path">caminho do arquivo no projeto</param>
        /// <returns>retorna um tipo string com o arquivo lido</returns>
        public String readFile(String path)
        {
            var resource = App.GetResourceStream(new Uri(path, UriKind.Relative));
            if (resource == null)
            {
                // Arquivo não encontrado
                return null;
            }
            StreamReader reader = new StreamReader(resource.Stream);
            String arquivo = reader.ReadToEnd();
            return arquivo;
        }

    }
}