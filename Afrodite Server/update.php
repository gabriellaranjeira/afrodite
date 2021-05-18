<?php
$ip = $_SERVER['REMOTE_ADDR'];
$date = date('Y-m-d H:i:s');
$nome = $_GET['n'];

$conn = new PDO('mysql:host=mysql.hostinger.com.br;dbname=u872761257_db', 'u872761257_db', 'lucas3302');
$stmt = $conn->prepare("UPDATE info
SET ip='$ip', ultimo='$date'
WHERE nome='$nome';");
$stmt->execute();
?>