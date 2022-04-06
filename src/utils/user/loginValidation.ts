const emailIsValid = (e: string) => {
  if (!e || e.length < 9 || !/@/.test(e)) return false;
  return true;
}