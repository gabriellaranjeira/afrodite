<?php

date_default_timezone_set('America/Sao_Paulo');
$nome = $_GET['n'];
$ip = $_SERVER['REMOTE_ADDR'];
$date = date('Y-m-d H:i:s');

$conn = new PDO('mysql:host=mysql.hostinger.com.br;dbname=u872761257_db', 'u872761257_db', 'lucas3302');
$stmt = $conn->prepare("INSERT INTO info(nome, ip, ultimo) VALUES ('$nome', '$ip', '$date')");
$stmt->execute();

?>