export const friendlyDateTime = (dateTimeString: string) => {
  const date = new Date(dateTimeString).toLocaleDateString("pt-br")
  const time = new Date(dateTimeString).toLocaleTimeString("pt-br")
  return `${date} às ${time.slice(0, time.length - 3)}`
}

export const brDateString = (date: string) => {
  return `${date.substring(8, 10)}/${date.substring(5, 7)}/${date.substring(
    0,
    4
  )}`
}

export const dateAndHourToDatetime = (data: string, hora: string) => {
  return new Date(`${data}T${hora}`).toISOString()
}

export const adjustForTimezone = (date: Date) => {
  var timeOffsetInMS: number = date.getTimezoneOffset() * 60000
  date.setTime(date.getTime() + timeOffsetInMS)
  return date
}
