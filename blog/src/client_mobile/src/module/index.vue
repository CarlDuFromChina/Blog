<template>
  <div>
    <mt-button @click.native="handleClick">Button</mt-button>
  </div>
</template>

<script>
export default {
  name: 'index',
  methods: {
    handleClick () {
      this.$message.alert('Hello World!', 'Hi~')
    }
  }
}
</script>
