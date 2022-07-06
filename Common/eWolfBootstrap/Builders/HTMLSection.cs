using System;

namespace eWolfBootstrap.Builders
{
    public class HTMLSection : HTMLBuilder
    {
        public HTMLSection(string className)
        {
            Text($"<div class='{className}'>");

            /*
<div class='card border-dark mb-3'>
<h5 class='card-header'>Lincoln Model Rail Club General Exhibition 2020</h5>
<div class='card-body'>
<h6>29/02/2020</h6>
      <img class='rounded float-right' width='214px' height ='160px'src='20200229-Newark\images\P2298149-Dorehill-sT-Lincoln-model-rail-club.Stephens-Lincoln-model-rail-club-thumb.JPG'>
<p class='col-md-6 card-text float-left'>Lincoln Model Rail Club General Exhibition</p>
<p class='col-md-6 '><a href='20200229-Newark/index.html' class='font-weight-bold'>See more</a></p>
</div>
</div>
</div>*/
        }

        public void End()
        {
            //Text("</div>");
            Text("</div>");
        }
    }
}