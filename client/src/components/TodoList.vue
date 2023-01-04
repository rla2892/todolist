<template>
  <h1>Todo List App - Sangjin</h1>
  <div>
    <input id="addInput" v-model="addInputText" type="text" placeholder="Write here!!!">
    <button @click="onAdd" >Add</button>
  </div>
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
      list: null,
      addInputText: ''
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
    },
    async AddTodo (content) {
      const url = `${API_URL}/api/todo`
      await fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
          // 'Content-Type': 'application/x-www-form-urlencoded',
        },
        body: JSON.stringify({
          content: content
        })
      })

      this.fetchData()
    },
    onAdd (e) {
      this.AddTodo(this.addInputText)
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
