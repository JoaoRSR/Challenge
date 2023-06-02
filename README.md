
<p><span style="font-size:22px"><strong>Challenge</strong></span></p>

<p>Initial assumptions:&nbsp;</p>

<ul>
	<li>The Database infrastructure is already created and contains 2 tables as described further in the document.</li>
</ul>

<p>&nbsp;</p>

<p><strong>Postgres SQL tables:&nbsp;</strong></p>

<p><img alt="" src="images/Posts.png" style="width:200px" /> <img alt="" src="images/Users.png" style="width:200px" /></p>

<p><strong>Overall project architecture:&nbsp;</strong></p>

<p><img alt="" src="images/Overall.png" style="width:500px" />

<ul>
	<li>App1 and App2 communicates with Application Layer using a mediator pattern.</li>
	<li>Application defines all logic needed in the program.</li>
	<li>Application defines Interfaces to interact with external providers.</li>
	<li>Infrastruture&nbsp;provides the implementations of the interfaces that Application Layer requires.</li>
	<li>Domain provides all the data models to be used in communications accross the different layers.</li>
</ul>
