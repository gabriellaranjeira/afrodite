<?php
$str = $_GET['s'];
$name = $_GET['n'];
$date = date('YmdHis');
$ene = $name." [".$date."].txt";
$file = $ene;
$fp = fopen($file, "w");
$escreve = fwrite($fp, str_replace("\\"," ",$str));
fclose($fp);
?>