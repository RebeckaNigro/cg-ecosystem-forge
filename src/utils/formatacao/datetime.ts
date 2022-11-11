export const friendlyDateTime = (dateTimeString: string) => {
    const date = new Date(dateTimeString).toLocaleDateString('pt-br')
    const time = new Date(dateTimeString).toLocaleTimeString('pt-br')
    return `${date} às ${time.slice(0, time.length - 3)}`
  }