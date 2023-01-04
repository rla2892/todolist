<template>
  <h1>Todo List App - Sangjin</h1>
  <ul>
    <li v-for="todoItem of list" v-bind:key="todoItem" >
      <span>{{ todoItem }}</span>
    </li>
  </ul>
</template>

<script>
const API_URL = `http://localhost:53015`
// const API_URL = `https://localhost:44397`

export default {
  data () {
    return {
      list: [11, 22, 33]
    }
  },
  created () {
    // fetch on init
    this.fetchData()
  },
  methods: {
    async fetchData () {
      const url = `${API_URL}/api/todo/list`
      const res = await fetch(url, {
        // mode: 'no-cors',
        headers: {
          'Content-Type': 'application/json'
        },
        redirect: 'follow'
      })
      const json = await res.json()
      this.list = json
    }
  }
}
</script>

<style scoped>
ul {
  padding-inline-start: 0;
}
li {
  border-color: limegreen;
  border-width: 2px;
  border-style: solid;
  margin-bottom: 1rem;
  list-style: none;
}
</style>
